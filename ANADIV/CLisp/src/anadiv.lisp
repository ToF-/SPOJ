(defun remove-list (a b)
  (if (null a)
    b
    (if (find (car a) b)
      (remove-list (cdr a) (remove (car a) b :count 1))
      'NONE)))

(defun list-< (a b)
  (cond
    ((null a) (not (null b)))
    ((null b) nil)
    ((< (car a) (car b)) t)
    ((> (car a) (car b)) nil)
    (t (list-< (cdr a) (cdr b)))))

(defun list-sort (a)
  (sort a #'list-<))

(defparameter *limit* 10000)
(defparameter *multiples-of-2* '((0) (2) (4) (6) (8)))
(defparameter *multiples-of-4* 
  (loop for x from 0 to 24 collect
              (multiple-value-bind (q r) 
                (truncate (* x 4) 10) 
                (cons r (cons q ())))))
(defparameter *multiples-of-5* '((0) (5)))
(defparameter *multiples-of-8*
  (loop for x from 0 to 124 collect 
              (multiple-value-bind (cent rest)
                (truncate (* x 8) 100) 
                (multiple-value-bind (tens units) 
                  (truncate rest 10) 
                  (cons units (cons tens (cons cent ())))))))

(defun number-from-digits (digits)
  (cond ((null digits) 0)
        ((> (car digits) 9) (error (format nil "number-from-digits called with ~A~%" (car digits))))
        (t (+ (car digits) (* 10 (number-from-digits (cdr digits)))))))

(defun number-pair-from-string (input)

  (defun split-values (digits prefix)
    (if (null (car digits))
      (cons prefix (list (number-from-digits (reverse (cdr digits)))))
      (split-values (cdr digits) (cons (car digits) prefix))))

  (let* ((all-digits (loop for c across input collect (digit-char-p c)))
           (pair (split-values all-digits ())))
    (cons (car pair) (cdr pair))))

(defun digits-from-number (n)
  (if (= n 0)
    ()
    (multiple-value-bind (quotient remainder)
      (truncate n 10)
      (cons remainder (digits-from-number quotient)))))

(defun string-from-digits (digits)
  (concatenate 'string (loop for d in (reverse digits) collect (digit-char d))))

(defun find-multiple-digits (digits multiple)
  (let ((result (remove-list multiple digits)))
    (cond
      ((not result) (list multiple ()))
      ((equal 'NONE result) 'NONE)
      (t (list multiple result)))))

(defun find-largest-multiple-anagram (digits multiples)

  (defun find-multiple-anagrams (multiples result)
    (if (null multiples)
      result
      (let ((found (find-multiple-digits digits (car multiples))))
        (cond ((equal 'NONE found) (find-multiple-anagrams (cdr multiples) result))
              ((null (cadr found)) (cons (car found) result))
              (t (let ((a (append (car found) (sort (copy-list (cadr found)) #'<))))
                    (find-multiple-anagrams (cdr multiples)
                                            (cons a result))))))))

  (let ((anagrams (find-multiple-anagrams multiples ())))
    (if (null anagrams)
      nil
      (car (list-sort anagrams)))))

(defun max-anagram-divisible-by-1 (digits)
  (if (null digits)
    nil
    (max-anagram digits)))

(defun max-anagram-starting-with-digit (digit digits prefix)
    (cond
      ((null digits) nil)
      ((= digit (car digits)) (append (cons (car digits) (reverse prefix)) (cdr digits)))
      (t (max-anagram-starting-with-digit digit (cdr digits) (cons (car digits) prefix)))))

(defun max-anagram-starting-with-multiple (multiple digits prefix)
  (if (null multiple)
    (append (reverse prefix) digits)
    (let ((result (max-anagram-starting-with-digit (car multiple) digits ())))
      (if result
        (max-anagram-starting-with-multiple (cdr multiple) (cdr result) (cons (car result) prefix))
        nil))))

(defun max-anagram-starting-with-one-of-multiples (multiples digits accum)
  (if (null multiples)
    accum
    (let ((result (max-anagram-starting-with-multiple (car multiples) digits ())))
      (cond
        ((not result) (max-anagram-starting-with-one-of-multiples (cdr multiples) digits accum))
        ((not accum) (max-anagram-starting-with-one-of-multiples (cdr multiples) digits result))
        ((list-< result accum) (max-anagram-starting-with-one-of-multiples (cdr multiples) digits result))
        (t (max-anagram-starting-with-one-of-multiples (cdr multiples) digits accum))))))

(defun max-anagram-divisible-by-2 (digits)
  (max-anagram-starting-with-one-of-multiples *multiples-of-2* (max-anagram digits) nil))

(defun max-anagram-divisible-by-3 (digits)
  (cond
    ((null digits) nil)
    ((> (rem (apply #'+ digits) 3) 0) nil)
    (t (max-anagram digits))))

(defun max-anagram-divisible-by-4 (digits)
  (cond
    ((< (length digits) 2)
     (if (= (rem (number-from-digits digits) 4) 0)
       digits
       nil))
     (t (max-anagram-starting-with-one-of-multiples *multiples-of-4* (max-anagram digits) nil))))

(defun max-anagram-divisible-by-5 (digits)
  (if (and (not (find 0 digits)) (not (find 5 digits)))
    nil
    (if (= (length digits) 1)
      (if (= (rem (car digits) 5) 0)
        (list (car digits))
        nil)
      (find-largest-multiple-anagram digits *multiples-of-5*))))

(defun max-anagram-divisible-by-6 (digits)
  (if (or
        (null (remove-if #'oddp digits))
        (> (rem (apply #'+ digits) 3) 0))
    nil
    (if (= (length digits) 1)
      (if (= (rem (car digits) 6) 0)
        (list (car digits))
        nil)
      (if (> (rem (apply #'+ digits) 3) 0) 
        nil
        (find-largest-multiple-anagram digits *multiples-of-2*)))))

(defun largest-anagram-multiple (k anagrams)
  (cond
    ((null anagrams) nil)
    ((> (rem (number-from-digits (car anagrams)) k) 0) (largest-anagram-multiple k (cdr anagrams)))
    (t (car anagrams))))

(defun decrement (operand)
  (cond
    ((null operand) ())
    ((> (car operand) 0) (cons (- (car operand) 1) (cdr operand)))
    (t (cons 9 (decrement (cdr operand))))))

(defun divisible-by-7 (digits)
  (cond
    ((< (length digits) 4) (= (rem (number-from-digits digits) 7) 0))
    (t (let* ((last-digit (car digits))
              (tens (+ (cadr digits) (* (caddr digits) 10)))
              (remain (cdddr digits))
              (x (- tens (* last-digit 2)))
              (y (if (< x 0) (+ 100 x) x))
              (adjusted-remain (if (< x 0) (decrement remain) remain)))
         (divisible-by-7 
           (multiple-value-bind (quotient remainder)
             (truncate x 10)
             (cons remainder (cons quotient adjusted-remain))))))))

(defun max-anagram-divisible-by-7 (digits)

  (defun accum-factors (digits plus len)
    (cond
      ((= len 0) 0)
      ((>= len 3)
       (let ((factor (+ (car digits) (* 10 (cadr digits)) (* 100 (caddr digits)))))
         (+ (* plus factor) (accum-factors (cdddr digits) (- plus) (- len 3)))))
      ((= len 2)
       (let ((factor (+ (car digits) (* 10 (cadr digits)))))
         (* plus factor)))
      ((= len 1)
       (* plus (car digits)))))
       
  (defun divisible-by-7 (digits)
    (let ((accum (accum-factors digits 1 (length digits))))
      (= (rem accum 7) 0)))

  (defun largest-anagram-multiple-of-7 (anagram)
    (cond
      ((null anagram) nil)
      ((divisible-by-7 anagram) anagram)
      (t (largest-anagram-multiple-of-7 (next-anagram anagram)))))

  (largest-anagram-multiple-of-7 (max-anagram digits)))

(defun max-anagram-divisible-by-8 (digits)
  (if (< (length digits) 4)
    (largest-anagram-multiple 8 (list-sort (all-anagrams digits)))
    (find-largest-multiple-anagram digits *multiples-of-8*)))

(defun max-anagram-divisible-by-9 (digits)
  (if (> (rem (apply #'+ digits) 9) 0)
    nil
    (max-anagram digits)))

(defun max-anagram-divisible-by-10 (digits)
  (if (find 0 digits)
    (max-anagram digits)
    nil))

; ****************************
(defun add (operand addend)

  (defun increment (operand)
    (cond
      ((null operand) (list 1))
      ((= (car operand) 9) (cons 0 (increment (cdr operand))))
      (t (cons (1+ (car operand)) (cdr operand)))))

  (if (null addend)
    operand
    (let ((o (car operand))
          (a (car addend))
          (os (cdr operand))
          (as (cdr addend)))
      (if (< (+ o a) 10)
        (cons (+ o a) (add os as))
        (cons (- (+ o a) 10) (add (increment os) as))))))

(defun sum-digits (operand)
  (if (null operand)
    (list 0)
    (add (sum-digits (cdr operand)) (list (car operand)))))


(defun divisible-by-8 (digits)
  (if (<= (length digits) 3)
    (= (rem (number-from-digits digits) 8) 0)
    (= (rem (+ (car digits) (* (cadr digits) 10) (* (caddr digits) 100)) 8) 0)))

(defun divisible-by-3 (digits)
  (= (rem (apply #'+ digits) 3) 0))

(defun divisible-by-4 (digits)
  (if (< (length digits) 3)
    (multiple digits 4)
    (multiple (list
                (car digits)
                (cadr digits)) 4)))

(defun divisible-by-5 (digits)
  (or (= (car digits) 0) (= (car digits) 5)))

(defun divisible-by-2 (digits)
  (evenp (car digits)))

(defun divisible-by-10 (digits)
  (= (car digits) 0))

(defun divisible-by-1 (digits)
  t)

(defun divisible-by-6 (digits)
  (and (divisible-by-3 digits)
       (divisible-by-2 digits)))


(defun max-anagram (digits)
  (sort digits #'<))

(defun split (digits)
  (defun split-aux (digits result)
    (cond
      ((null digits) (cons (reverse result) digits))
      ((> (car digits) (car result)) (cons (reverse result) digits))
      (t (split-aux (cdr digits) (cons (car digits) result)))))

  (split-aux (cdr digits) (list (car digits))))

; assert (> (car suffix) (car (last prefix)))
(defun swap (prefix suffix result)
  (if (>= (car prefix) (car suffix))
    (swap (cdr prefix) suffix (cons (car prefix) result))
    (append (reverse (cdr prefix)) (cons (car suffix) result) (cons (car prefix) (cdr suffix)))))

(defun next-anagram (digits)
  (let* ((sp (split digits))
         (prefix (car sp))
         (suffix (cdr sp)))
    (if (null suffix)
      nil
      (swap prefix suffix ()))))

(defun all-anagrams (digits)
  (defun all-anagrams-aux (digits)
    (if (null digits)
      nil
      (cons digits (all-anagrams-aux (next-anagram digits)))))
  (all-anagrams-aux (max-anagram digits)))

(defun minimal-common (digits elements)
  (cond
    ((null digits) nil)
    ((null elements) nil)
    ((= (car digits) (car elements)) (car digits))
    ((< (car digits) (car elements)) (minimal-common (cdr digits) elements))
    ((> (car digits) (car elements)) (minimal-common digits (cdr elements)))))

; given a sorted list of digits and a  multiple (m1 m2 …mn)
; returns digits minus (m1 m2…mn) if these could be extracted from digits
; or nil if multiple cannot be extracted from digits
(defun extract-multiple (digits multiple)

  (defun extract-multiple-aux (digits multiple result)
    (cond
      ((null multiple) (append result digits))
      ((null digits) nil)
      ((< (car digits) (car multiple))
       (extract-multiple-aux (cdr digits) multiple (cons (car digits) result)))
      ((> (car digits) (car multiple)) nil)
      (t (extract-multiple-aux (remove (car multiple) digits :count 1) (cdr multiple) result))))

  (extract-multiple-aux (sort digits #'<) (sort multiple #'<) ()))

; given a list of digits and a list of multiples ((m1…mn) (n1…nn)…(z1…zn))
; sorted by ascending order e.g '((0 0) (4 0) (8 0) (2 1) (6 1)…(6 9))
; returns a pair ((x1…xn) prefix) such that
; (x1…xn) is the lowest multiple that can be extracted from digits,
; meaning x1 ∈ digits, x2 ∈ digits minus x1, xn ∈ digits minus (x1 x2…xm)
; prefix = digits minus (x1 x2 xm…xn)
; or nil if no multiple can be extracted
(defun find-minimal-multiple (digits multiples)
  (cond
    ((null digits) nil)
    ((null multiples) nil)
    (t (let* ((multiple (car multiples))
              (remaining (extract-multiple digits multiple)))
         (if remaining
           (cons multiple remaining)
           (find-minimal-multiple digits (cdr multiples)))))))
  

(defun early-stop (k digits)
  (cond
    ((null digits) t)
    ((= k 2) (null (remove-if #'oddp digits)))
    ((= k 3) (> (rem (apply #'+ digits) 3) 0))
    ((= k 4) (null (remove-if #'oddp digits)))
    ((= k 5) (null (remove-if-not #'(lambda (x) (or (= 0 x) (= 5 x))) digits)))
    ((= k 6) (or (early-stop 3 digits) (early-stop 2 digits)))
    ((= k 8) (early-stop 2 digits))
    ((= k 9) (> (rem (apply #'+ digits) 9) 0))
    ((= k 10) (null (remove-if #'(lambda (x) (> x 0)) digits)))
    (t nil)))

(defun max-anagram-divisible-by (k digits)
  (cond
    ((= k 1) (max-anagram-divisible-by-1 digits))
    ((= k 2) (max-anagram-divisible-by-2 digits))
    ((= k 3) (max-anagram-divisible-by-3 digits))
    ((= k 4) (max-anagram-divisible-by-4 digits))
    ((= k 5) (max-anagram-divisible-by-5 digits))
    ((= k 6) (max-anagram-divisible-by-6 digits))
    ((= k 7) (max-anagram-divisible-by-7 digits))
    ((= k 8) (max-anagram-divisible-by-8 digits))
    ((= k 9) (max-anagram-divisible-by-9 digits))
    ((= k 10) (max-anagram-divisible-by-10 digits))
    ))

(defun read-pair ()
  (handler-case
    (let ((line (read-line)))
      (number-pair-from-string line))
    (end-of-file () nil)))

(defun process-pair (pair)
  (let* ((anagram
           (max-anagram-divisible-by
             (cadr pair)
             (car pair))))
    (format t "~A~%" (if anagram (string-from-digits anagram) (- 1)))))

(defun process ()
  (let ((pair (read-pair)))
    (if pair
      (progn
        (process-pair pair)
        (process)
      ))))

(defun mod-7s (n)
  (defun mod-7s-aux (digits counter)
    (if digits
      (if (= (rem (number-from-digits digits) 7) 0)
        counter
        (mod-7s-aux (next-anagram digits) (1+ counter)))
      (- 1)))
  (car (sort
         (loop for i from 0 to n
               collect
               (mod-7s-aux
                 (max-anagram
                   (digits-from-number (random (expt 10 1000))))
                 0)) #'>)))

(defun generate-random-test-cases (n)
  (defun generate-test-case ()
    (let ((n (digits-from-number (random (expt 10 1000))))
          (k (1+ (random 10))))
      (format t "~A ~A:~A~%" (number-from-digits n) k (number-from-digits (max-anagram-divisible-by k n)))))
  (loop for i from 1 to n
        do (generate-test-case)))

(defun generate-sequential-test-cases (n value)
  (defun generate-test-case (n v)
    (cond ((= n 0) ())
          ((= v 0) ())
          (t (loop for k from 1 to 10
                   do (let ((r (max-anagram-divisible-by k (digits-from-number v))))
                        (format t "~A ~A:~A~%" v k (if r (number-from-digits r) -1))))
             (generate-test-case (- n 1) (- v 1)))))
  (generate-test-case n (number-from-digits (max-anagram (digits-from-number value)))))


(defun generate-sequential ()
  (progn
    (if (= (length *posix-argv*) 3)
      (generate-sequential-test-cases (parse-integer (cadr *posix-argv*)) (parse-integer (caddr *posix-argv*)))
      (format t "incorrect arguments: ~A~%" *posix-argv*))
    (sb-ext:quit)))






