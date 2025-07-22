; anadiv.lisp


(defun digits (n)
  (defun digits-aux (n)
    (if (= n 0)
      ()
      (multiple-value-bind (q r)
        (truncate n 10)
        (cons r (digits-aux q)))))
  (if (= n 0)
    '(0)
    (digits-aux n)))

(defun max-subseq (l n)
  (append
    (sort (subseq l 0 n) #'<)
    (subseq l n)))

(defun desc-prefix (l)
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
  (desc-prefix-aux l ()))


(defun to-swap (sp)
  ; find in the ordered prefix the max digit < limit
  ; starts with pivot = first digit of prefix
  (defun find-pivot (prefix limit pivot result)
    (cond
      ((null prefix) (cons pivot result))
      ((>= (car prefix) limit) (find-pivot (cdr prefix) limit pivot (cons (car prefix) result)))
      ((> (car prefix) pivot) (find-pivot (cdr prefix) limit (car prefix) (cons pivot result)))
      ((<= (car prefix) pivot) (find-pivot (cdr prefix) limit pivot (cons (car prefix) result)))))
  (if (null (cdr sp))
    nil
    (let ((prefix (sort (car sp) #'<))
          (limit (cadr sp)))
      (cons
        (find-pivot (cdr prefix) limit (car prefix) ())
        (cdr sp)))))

(defun swap (sp)
  (if (null sp)
    nil
    (let ((prefix (car sp))
          (suffix (cdr sp)))
      (append (max-subseq (cons (car suffix) (cdr prefix)) (length prefix))
              (cons (car prefix) (cdr suffix))))))

(defun number- (d)
  (if (null d)
    0
    (+ (car d) (* 10 (number- (cdr d))))))

(defun max-anagram (n)
  (let ((d (digits n)))
    (number- (max-subseq d (length d)))))

(defun next-anagram (n)
  (number- (swap (to-swap (desc-prefix (digits n))))))

(defun print-all-anagrams (n)
  (defun process (n)
    (if (= 0 n)
      ()
      (progn
        (format t "~A " n)
        (process (next-anagram n)))))
  (process (max-anagram n)))
