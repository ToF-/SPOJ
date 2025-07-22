; anadiv.lisp

(defun show (a)
  (format t "← ~A~%" a)
  a)

(defun digits (n)
  (format t "(digits ~A)~%" n)
  (defun digits-aux (n)
    (if (= n 0)
      ()
      (multiple-value-bind (q r)
        (truncate n 10)
        (cons r (digits-aux q)))))
  (show (if (= n 0)
    '(0)
    (digits-aux n))))

(defun max-subseq (l n)
  (format t "(max-subseq ~A ~A)~%" l n)
  (show (append
    (sort (subseq l 0 n) #'<)
    (subseq l n))))

(defun desc-prefix (l)
  (format t "(desc-prefix ~A)~%" l)
  (defun desc-prefix-aux (l prefix)
    (cond
      ((null l)
       (cons (reverse prefix) l))
      ((null prefix)
       (desc-prefix-aux (cdr l) (cons (car l) prefix)))
      ((<= (car l) (car prefix))
       (desc-prefix-aux (cdr l) (cons (car l) prefix)))
      (t
        (cons (reverse prefix) l))))
  (show (desc-prefix-aux l ())))


(defun to-swap (sp)
  (format t "(to-swap ~A)~%" sp)
  (defun find-pivot (prefix limit pivot result)
    (format t "(find-pivot (~A ~A ~A ~A)~%" prefix limit pivot result)
    (cond
      ((null prefix) (cons pivot result))
      ((> (car prefix) limit) (find-pivot (cdr prefix) limit pivot (cons (car prefix) result)))
      ((> (car prefix) pivot) (find-pivot (cdr prefix) limit (car prefix) (cons pivot result)))
      ((<= (car prefix) pivot) (find-pivot (cdr prefix) limit pivot (cons (car prefix) result)))))
  (show (if (null (cdr sp))
    nil
    (let ((prefix (sort (car sp) #'<))
          (limit (cadr sp)))
      (cons
        (find-pivot (cdr prefix) limit (car prefix) ())
        (cdr sp))))))

(defun swap (sp)
  (format t "(swap ~A)~%" sp)
  (show (if (null sp)
    nil
    (let ((prefix (car sp))
          (suffix (cdr sp)))
      (append (max-subseq (cons (car suffix) (cdr prefix)) (length prefix))
              (cons (car prefix) (cdr suffix)))))))

(defun number- (d)
  (format t "(number- ~A)~%" d)
  (if (null d)
    0
    (+ (car d) (* 10 (number- (cdr d))))))

(defun max-anagram (n)
  (let ((d (digits n)))
    (number- (max-subseq d (length d)))))

(defun next-anagram (n)
  (number- (swap (to-swap (desc-prefix (digits n))))))

