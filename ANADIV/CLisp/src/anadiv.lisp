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

(defun split-digits (digits)
  (defun split-digits-aux (dgts last-digit left)
    (if (null dgts)
      (list left '())
      (let ((digit (car dgts)))
        (if (< digit last-digit)
          (list left dgts)
          (split-digits-aux (cdr dgts) digit (append left (list digit)))))))
  (split-digits-aux digits 0 '()))

(defun max-lower-anagram (digits)
  (let ((split (split-digits digits)))
    (if (null (cadr split))
      '()
      (let* ((pref (car split))
             (suff (cadr split))
             (l (car (last pref)))
             (pre (butlast pref))
             (sub (sort suff #'>))
             (new (car sub))
             (post (cdr sub)))
        (format t "pref:~A suff:~A pre:~A l:~A sub:~A new:~A post:~A~%" pref suff pre l sub new post)
        (append pre (list new) (sort (cons l post) #'> ))))))


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



