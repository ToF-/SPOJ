(defun number-pair-from-string (input)
  (with-input-from-string (s input)
    (cons (read s) (read s))))

(defun digits-from-number (n)
  (if (= n 0)
    ()
    (multiple-value-bind (quotient remainder)
      (truncate n 10)
      (cons remainder (digits-from-number quotient)))))

(defun value (digits)
  (cond ((null digits) 0)
        ((> (car digits) 9) (error (format nil "value called with ~A~%" (car digits))))
        (t (+ (car digits) (* 10 (value (cdr digits)))))))

(defun subtract (minuend subtrahend)

  (defun decrement (operand)
    (cond
      ((null operand) ())
      ((> (car operand) 0) (cons (- (car operand) 1) (cdr operand)))
      (t (cons 9 (decrement (cdr operand))))))

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

(defun divisible-by-7 (digits)
  (if (< (length digits) 3)
    (multiple digits 7)
    (let ((subtrahend (digits-from-number (* 2 (car digits)))))
      (divisible-by-7 (subtract (cdr digits) subtrahend)))))

(defun divisible-by-8 (digits)
  (if (< (length digits) 3)
    (multiple digits 8)
    (multiple (list (car digits)
                    (cadr digits)
                    (caddr digits)) 8)))

(defun divisible-by-9 (digits)
  (multiple (sum-digits digits) 9))

(defun divisible-by-3 (digits)
  (multiple (sum-digits digits) 3))

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
  (sort digits #'>))

(defun split (digits)
  (defun split-aux (digits result)
    (cond
      ((null digits) (cons (reverse result) digits))
      ((> (car digits) (car result)) (cons (reverse result) digits))
      (t (split-aux (cdr digits) (cons (car digits) result)))))

  (split-aux (cdr digits) (list (car digits))))

(defun swap (digit digits)
  (defun swap-aux (digits result)
    (cond
      ((null digits) nil)
      ((> (car digits) digit) (cons (car result) (append (cdr result) digits)))
      ((<= (car digits) digit) (swap-aux (cdr digits) (cons (car digits) result)))))

  (let* ((sw (swap-aux (sort digits #'<) '()))
         (head (car sw))
         (tail (cdr sw)))
    (cons head (sort tail #'<))))

(defun next-anagram (digits)
  (let* ((sp (split digits))
         (prefix (car sp))
         (suffix (cdr sp)))
  (cond
    ((null suffix) nil)
    (t (cons (cadr digits) (list (car digits)))))))

