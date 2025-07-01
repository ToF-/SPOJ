(defun number-pair-from-string (input)
  (with-input-from-string (s input)
    (cons (read s) (read s))))

(defun digits (n)

  (defun digits-aux (nb)
    (if (= nb 0)
      ()
      (multiple-value-bind (qu re)
        (truncate nb 10)
        (cons re (digits-aux qu)))))

  (sort (digits-aux n) #'<))

(defun digits-to-number (digits)
  
  (defun digits-to-number-aux (dgts result)
    (if (null dgts)
      result
      (digits-to-number-aux (cdr dgts) (+ (* result 10) (car dgts)))))

  (digits-to-number-aux digits 0))


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
