(defun digits (n)
  (defun digits-aux (n)
    (if (= n 0)
      ()
      (cons (rem n 10) (digits-aux (truncate n 10)))))
  (sort (digits-aux n) #'>))

(defun all-permutations (list)
  (cond
    ((null list) (list list))
    ((null (cdr list)) (list list))
    (t (loop for element in list
             append
             (mapcar
               (lambda (l) (cons element l))
               (all-permutations
                 (remove element list)))))))

(defun extract (n l)
  (defun extract-aux (n lst rst)
    (cond
      ((null lst) '())
      ((= n 0) (cons (car lst) (append (reverse rst) (cdr lst))))
      (t (extract-aux (- n 1) (cdr lst) (cons (car lst) rst)))))
  (extract-aux n l '()))

(defun extractions (lst)
  (defun extractions-aux (n m lst)
    (cond
      ((null lst) '())
      ((= n m) '())
      (t (cons
           (extract n lst)
           (extractions-aux (1+ n) m lst)))))
  (extractions-aux 0 (length lst) lst))
