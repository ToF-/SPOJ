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

(defun max-lower-anagram (digits)
  (let ((split (split-digits digits)))
  (cond
    ((null (cdr split)) ())
    (t (append (cdr split) (list (caar split)))))))

