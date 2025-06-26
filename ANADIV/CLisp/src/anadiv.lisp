(defun digits (n)
  (defun digits-aux (n)
    (if (= n 0)
      ()
      (cons (rem n 10) (digits-aux (truncate n 10)))))
  (sort (digits-aux n) #'>))

(defun all-permutations (elements)
  (cond
    ((null elements) (list elements))
    ((null (cdr elements)) (list elements))
    (t (loop for element in elements
             append
             (mapcar
               (lambda (sublist) (cons element sublist))
               (all-permutations
                 (remove element elements)))))))

