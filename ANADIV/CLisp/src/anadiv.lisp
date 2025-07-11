(defun value (digits)
  (cond ((null digits) 0)
        ((> (car digits) 9) (error (format nil "value called with ~A~%" (car digits))))
        (t (+ (car digits) (* 10 (value (cdr digits)))))))

(defun number-pair-from-string (input)

  (defun split-values (digits prefix)
    (if (null (car digits))
      (cons prefix (list (value (reverse (cdr digits)))))
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

(defun decrement (operand)
  (cond
    ((null operand) ())
    ((> (car operand) 0) (cons (- (car operand) 1) (cdr operand)))
    (t (cons 9 (decrement (cdr operand))))))

(defun subtract (minuend subtrahend)

  (defun calibrate (operand)
    (defun calibrate-aux (operand)
      (cond ((null operand) ())
            ((= (car operand) 0) (calibrate-aux (cdr operand)))
            (t operand)))
    (reverse (calibrate-aux (reverse operand))))

  (if (null subtrahend)
    (calibrate minuend)
    (let ((m (car minuend))
          (s (car subtrahend))
          (ms (cdr minuend))
          (ss (cdr subtrahend)))
      (if (>= m s)
        (cons (- m s) (subtract ms ss))
        (cons (- (+ m 10) s) (subtract (decrement ms) ss))))))

(defun multiple (operand m)
  (if (= (length operand) 1)
    (if (>= (car operand) m)
      (multiple (list (- (car operand) m)) m)
      (= (car operand) 0))
    (multiple (subtract operand (list m)) m)))

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
  (if (< (length digits) 3)
    (multiple digits 8)
    (multiple (list (car digits)
                    (cadr digits)
                    (caddr digits)) 8)))

(defun divisible-by-9 (digits)
  (multiple (sum-digits digits) 9))

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

(defun divisible-by-7 (digits)
  (cond
    ((< (length digits) 4) (= (rem (value digits) 7) 0))
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

(defun divisible-by-10 (digits)
  (= (car digits) 0))

(defun divisible-by-1 (digits)
  t)

(defun divisible-by-6 (digits)
  (and (divisible-by-3 digits)
       (divisible-by-2 digits)))

(defparameter divisible-functions
  (list nil
        #'divisible-by-1
        #'divisible-by-2
        #'divisible-by-3
        #'divisible-by-4
        #'divisible-by-5
        #'divisible-by-6
        #'divisible-by-7
        #'divisible-by-8
        #'divisible-by-9
        #'divisible-by-10))

(defun divisible-by (k digits)
  (funcall (nth k divisible-functions) digits))

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

(defun max-anagram-divisible-by-7 (digits)

  (defun find-anagram (anagram)
    (cond
      ((null anagram) nil)
      ((divisible-by-7 anagram) anagram)
      (t (find-anagram (next-anagram anagram)))))

  (find-anagram (max-anagram digits)))

(defun early-stop (k digits)
  (format t "(early-stop ~A ~A)~%" k digits)
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
  (defun find-anagram (anagram)
    (format t "(find-anagram ~A) k=~A digits=~A~%" anagram k digits)
    (cond
      ((null anagram) nil)
      ((= 1 k) anagram)
      ((equal anagram digits) (find-anagram (next-anagram anagram)))
      (t (if (divisible-by k anagram)
           anagram
           (find-anagram (next-anagram anagram))))))

  (if (early-stop k digits)
    nil
    (find-anagram (max-anagram digits))))

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

