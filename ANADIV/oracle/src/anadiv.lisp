; anadiv.lisp

(defun max-subsequence (l n)
  (append
    (sort (subseq l 0 n) #'<)
    (subseq l n)))

(defun descending-prefix (l)
  (defun descending-prefix-aux (l prefix)
    (cond
      ((null l)
       (cons (reverse prefix) l))
      ((null prefix)
       (descending-prefix-aux (cdr l) (cons (car l) prefix)))
      ((<= (car l) (car prefix))
       (descending-prefix-aux (cdr l) (cons (car l) prefix)))
      (t
        (cons (reverse prefix) l))))
  (descending-prefix-aux l ()))


(defun swap-digits-and-reorder (sp)
  '(7 0 4 8))
