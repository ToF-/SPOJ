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

(defun divisible-7-p (n)
  (cond
    ((or (= 0 n) (= 7 n) (= (- 7) n)) t)
    ((> (abs n) 10)
     (multiple-value-bind (q r)
       (truncate n 10)
       (let ((m (- q (* 2 r))))
         (progn
           (format t "n:~A q:~A r:~A m:~A ~%" n q r m)
           (divisible-7-p m)))))
    (t nil)))


