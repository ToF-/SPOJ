(defun number-pair-from-string (input)
  (with-input-from-string (s input)
    (cons (read s) (read s))))

(defun digits-from-number (n)
  (if (= n 0)
    ()
    (multiple-value-bind (quotient remainder)
      (truncate n 10)
      (cons remainder (digits-from-number quotient)))))

(defun subtract (operand n)
  (cond
    ((null n) operand)
    (t (cons (- (car operand) (car n)) (subtract (cdr operand) (cdr n))))))

; (defun digits-to-number (digits)
; 
;   (defun digits-to-number-aux (dgts result)
;     (if (null dgts)
;       result
;       (digits-to-number-aux (cdr dgts) (+ (* result 10) (car dgts)))))
; 
;   (digits-to-number-aux digits 0))
; 
; (defun number-to-digits (n)
; 
;   (defun number-to-digits-aux (nb result)
;     (if (= nb 0)
;       result
;       (multiple-value-bind (qu re)
;         (truncate nb 10)
;         (number-to-digits-aux qu (cons re result)))))
; 
;   (number-to-digits-aux n ()))
; 
; (defun digits (n)
;   (sort (number-to-digits n) #'<))
; 
; (defun split-digits (dgts)
; 
;   (defun split-digits-aux (digits result)
;     (cond ((null digits)
;            (list (reverse result)))
;           ((or
;              (null (cdr digits))
;              (<= (car digits) (cadr digits)))
;            (split-digits-aux (cdr digits) (cons (car digits) result)))
;           (t (cons
;                (reverse (cons (car digits) result))
;                (cdr digits)))))
; 
;   (split-digits-aux dgts '()))
; 
; (defun max-anagram (digits)
;   (sort digits #'>))
; 
; (defun extract-lower (digit dgts)
;   
;   (defun extract-lower-aux (digits result)
;     (cond
;       ((null digits) result)
;       ((< (car digits) digit) (cons (car digits) (append (cdr digits) result)))
;       (t (extract-lower-aux (cdr digits) (cons (car digits) result)))))
; 
;   (extract-lower-aux (sort dgts #'>) ()))
; 
; (defun max-lower-anagram (digits)
;   (let ((split (split-digits digits)))
;   (if (null (cdr split))
;     ()
;     (let ((suff (max-lower-anagram (cdr split))))
;       (if suff
;         (append (car split) suff)
;         (let* ((prefix (reverse (car split)))
;                (suffix (cdr split))
;                (split-digit (car prefix))
;                (split-rem   (cdr prefix))
;                (extract (extract-lower split-digit suffix))
;                (new-digit (car extract))
;                (new-suffix (cdr extract)))
;           (append (reverse (cons new-digit split-rem))
;                   (sort (cons split-digit new-suffix) #'>))))))))
; 
; (defun max-lower-anagrams (digits)
;   (if (null digits)
;     ()
;     (progn
;       (format t "~A~%" (digits-to-number digits))
;       (max-lower-anagrams (max-lower-anagram digits)))))
; 
; (defun digit-subtract (digits sub)
;   
;   (defun decrease (digits)
;     (cond
;       ((null digits) ())
;       ((= 0 (car digits)) (cons 9 (decrease (cdr digits))))
;       (t (cons (- (car digits) 1) (cdr digits)))))
; 
;   (defun digit-subtract-aux (digits sub result)
;     (format t "digits:~A sub:~A result:~A~%" digits sub result)
;     (cond
;       ((null digits) result)
;       ((null sub) (append result digits))
;       ((<= (car sub) (car digits))
;        (digit-subtract-aux (cdr digits) (cdr sub)
;                            (append result (list (- (car digits) (car sub))))))
;       (t
;         (digit-subtract-aux (decrease (cdr digits)) (cdr sub)
;                             (append result (list (- (+ 10 (car digits)) (car sub))))))))
; 
;   (reverse (digit-subtract-aux (reverse digits) (reverse sub) ())))
; 
; (defun divisible-8 (digits)
; 
;   (defun first-three (dgts)
;     (cons (car dgts)
;           (cons (cadr dgts)
;           (list (caddr dgts)))))
; 
;   (let ((m (apply #'+ (if (> (length digits) 3)
;                         (first-three (reverse digits)) digits))))
;     (= 0 (rem m 8))))
; 
; (defun divisible (digits n)
;   (let ((stigid (reverse digits)))
;     (cond
;       ((= 1 n) t)
;       ((= 2 n) (evenp (car stigid)))
;       ((= 3 n) (= 0 (rem (apply #'+ digits) 3)))
;       ((= 4 n)
;        (if (>= (length digits) 2)
;          (= 0 (rem (+ (car stigid) (cadr stigid)) 4))
;          (= 0 (rem (car stigid) 4))))
;       ((= 5 n)
;        (or (= 0 (car stigid)) (= 5 (car stigid))))
;       ((= 6 n)
;        (and (= 0 (rem (apply #'+ digits) 3)) (evenp (car stigid))))
;       ((= 7 n)
;        (divisible-7 digits))
;       ((= 8 n)
;        (divisible-8 digits))
;       ((= 9 n)
;        (= 0 (rem (apply #'+ digits) 9)))
;       ((= 10 n)
;        (= 0 (car (reverse digits))))
;       )))
; 
; (defun sum-divisible-aux (digits k acc)
;   (cond
;     ((>= acc k) (sum-divisible-aux digits k (- acc k)))
;     ((null digits) (= acc 0))
;     (t (sum-divisible-aux (cdr digits) k (+ acc (car digits))))))
; 
; (defun sum-divisible (dgts k)
;   (sum-divisible-aux dgts k 1))
;       
; (defun divisible-7 (digits)
;   (if (< (length digits) 4)
;     (
;     (= 0 (rem (digits-to-number digits) 7))
;     (let* ((digit (car (last digits)))
;            (remain (butlast digits))
;            (sub (number-to-digits (* digit 2))))
;       (divisible-7 (digit-subtract remain sub))))))
; 
; (defun is-divisible (digits k)
;   (let ((predicates
;           (list
;             #'(lambda (x) nil)
;             #'(lambda (x) t)
;             #'(lambda (x) (evenp (car (reverse x))))
;             #'(lambda (x) (sum-divisible x 3))
;             #'(lambda (x)
;                 (sum-divisible (if (> (length x) 1)
;                                  (list (+ (* (cadr x) 10) (car x)))
;                                  (list (car x))) 4))
;             #'(lambda (x) (or (= 5 (car x)) (= 0 (car x))))
;             #'(lambda (x) (and (sum-divisible x 3) (evenp (car (reverse x)))))
;             #'(lambda (x) (divisible-7 x))
;             #'(lambda (x) (sum-divisible
;                             (let ((xxx (append x (list 0 0))))
;                               (+ (* 100 (caddr xxx)) (* 10 (cadr xxx)) (car xxx))) 8))
;             #'(lambda(x) (sum-divisible x 9))
;             #'(lambda(x) (= 0 (car x)))
;             )))
;     (funcall (nth k predicates) (reverse digits))))
; 
