(defun number-pair-from-string (input)
  (with-input-from-string (s input)
    (cons (read s) (read s))))

(defun digits-to-number (digits)

  (defun digits-to-number-aux (dgts result)
    (if (null dgts)
      result
      (digits-to-number-aux (cdr dgts) (+ (* result 10) (car dgts)))))

  (digits-to-number-aux digits 0))

(defun number-to-digits (n)

  (defun number-to-digits-aux (nb result)
    (if (= nb 0)
      result
      (multiple-value-bind (qu re)
        (truncate nb 10)
        (number-to-digits-aux qu (cons re result)))))

  (number-to-digits-aux n ()))

(defun digits (n)
  (sort (number-to-digits n) #'<))

(defun split-digits (dgts)

  (defun split-digits-aux (digits result)
    (cond ((null digits)
           (list (reverse result)))
          ((or
             (null (cdr digits))
             (<= (car digits) (cadr digits)))
           (split-digits-aux (cdr digits) (cons (car digits) result)))
          (t (cons
               (reverse (cons (car digits) result))
               (cdr digits)))))

  (split-digits-aux dgts '()))

(defun max-anagram (digits)
  (sort digits #'>))

(defun extract-lower (digit dgts)
  
  (defun extract-lower-aux (digits result)
    (cond
      ((null digits) result)
      ((< (car digits) digit) (cons (car digits) (append (cdr digits) result)))
      (t (extract-lower-aux (cdr digits) (cons (car digits) result)))))

  (extract-lower-aux (sort dgts #'>) ()))

(defun max-lower-anagram (digits)
  (let ((split (split-digits digits)))
  (if (null (cdr split))
    ()
    (let ((suff (max-lower-anagram (cdr split))))
      (if suff
        (append (car split) suff)
        (let* ((prefix (reverse (car split)))
               (suffix (cdr split))
               (split-digit (car prefix))
               (split-rem   (cdr prefix))
               (extract (extract-lower split-digit suffix))
               (new-digit (car extract))
               (new-suffix (cdr extract)))
          (append (reverse (cons new-digit split-rem))
                  (sort (cons split-digit new-suffix) #'>))))))))

(defun max-lower-anagrams (digits)
  (if (null digits)
    ()
    (progn
      (format t "~A~%" (digits-to-number digits))
      (max-lower-anagrams (max-lower-anagram digits)))))

(defun divisible (digits n)
  (let ((stigid (reverse digits)))
    (cond
      ((= 1 n) t)
      ((= 2 n) (evenp (car stigid)))
      ((= 3 n) (= 0 (rem (apply #'+ digits) 3)))
      ((= 4 n)
       (if (>= (length digits) 2)
         (= 0 (rem (+ (car stigid) (cadr stigid)) 4))
         (= 0 (rem (car stigid) 4))))
      ((= 5 n)
       (or (= 0 (car stigid)) (= 5 (car stigid))))
      ((= 6 n)
       (and (= 0 (rem (apply #'+ digits) 3)) (evenp (car stigid))))
      )))
