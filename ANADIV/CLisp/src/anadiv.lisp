(defun number-pair-from-string (input)
  (with-input-from-string (s input)
    (cons (read s) (read s))))

(defun nal (n)
  (if (= n 0)
    ()
    (multiple-value-bind (q r)
      (truncate n 10)
      (cons r (nal q)))))

(defun lan (elements)
  (if (null elements)
    0
    (+ (car elements) (* 10 (lan (cdr elements))))))

(defun nal-9-complement (digits)
  (if (null digits)
    ()
    (cons (- 9 (car digits)) (nal-9-complement (cdr digits)))))

(defun nal-minus (a b)
  (cond
    ((null b) a)
    (t (cons (- (car a) (car b)) (nal-minus (cdr a) (cdr b))))))

(defun digits (n)
  (sort (nal n) #'<))

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



