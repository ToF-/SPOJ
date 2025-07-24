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

(defun reorder-subseq (l n)
  (append
    (sort (subseq l 0 n) #'<)
    (subseq l n)))

(defun reorder-seq (l)
  (reorder-subseq l (length l)))

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
      (append (reorder-seq (cons (car suffix) (cdr prefix)))
              (cons (car prefix) (cdr suffix))))))

(defun number- (d)
  (if (null d)
    0
    (+ (car d) (* 10 (number- (cdr d))))))

(defun max-anagram (n &key (predicate #'(lambda (x) t)) strict)
  (let ((d (digits n)))
    (if (apply predicate (list d))
      (let ((result (number- (reorder-subseq d (length d)))))
        (if (and strict (= result n))
          (next-anagram result)
          result))
      0)))

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

(defun remove-digits (target digits)
  (if (null target)
    digits
    (if (find (car target) digits)
      (remove-digits (cdr target) (remove (car target) digits :count 1))
      (list -1))))

; return the maximum anagram of n | f of size s is a suffix of n
; e.g (max-suffix (3 96 14069)) → 41096 because 096 is a suffix of n and 41096 > 14096
; e.g (max-suffix (3 96 41096 :strict t)) → 14096 because 096 is a suffix of n and 14096 ≠ 14096
(defun max-suffix (s f n &key strict)
  (defun digits-aux (n c)
    (if (= c 0)
      ()
      (multiple-value-bind (q r)
        (truncate n 10)
        (cons r (digits-aux q (- c 1))))))

  (let* ((tgt (digits-aux f s))
         (dgt (digits n))
         (prefix (remove-digits tgt dgt)))
    (cond
      ((equal prefix '(-1)) 0)
      ((null prefix) (if (= f n) (if strict 0 f)))
      (t (let ((result (number- (append tgt (reorder-subseq prefix (length prefix))))))
              (if (/= result n)
                result
                (if strict
                  0
                  (next-anagram result))))))))

(defun max-suffixes (s fs n &key strict)
  (let ((suffixes (mapcar #'(lambda (f) (max-suffix s f n :strict strict)) fs)))
    (format t "(max-suffixes (~A ~A ~A ~A)~%" s fs n strict)
    (format t "~A~%" suffixes)
    (car (sort
           (if strict 
             (remove-if #'(lambda (x) (= x n)) suffixes)
             suffixes)
         #'>))))

(defparameter *multiples-4* (loop for f from 0 to 24 collect (* f 4)))
(defparameter *multiples-8-2* (loop for f from 0 to 12 collect (* f 8)))
(defparameter *multiples-8-3* (loop for f from 0 to 124 collect (* f 8)))

(defun find-multiple-7 (n)
  (defun find-max-anagram (n counter)
    (cond
      ((= counter 0) 0)
      ((= (rem n 7) 0) n)
      (t (find-max-anagram (next-anagram n) (- counter 1)))))
    (find-max-anagram (max-anagram n) 150))

(defun max-anagram-multiple (f n &key strict)
  (let ((result (cond
                  ((= 1 f) (max-anagram n :strict strict))
                  ((= 2 f) (let ((result (max-suffixes 1 '(0 2 4 6 8) n :strict strict)))
                             (if (and strict (= n result))
                               (next-anagram result)
                               result)))
                  ((= 3 f) (max-anagram n :predicate #'(lambda (ds) (= (rem (apply #'+ ds) 3) 0)) :strict strict))
                  ((= 4 f) (if (< n 10) (if (and (= (rem n 4) 0) (not strict)) n 0)
                             (max-suffixes 2 *multiples-4* n :strict strict)))
                  ((= 5 f) (max-suffixes 1 '(0 5) n :strict strict))
                  ((= 6 f) (max-suffixes 1 '(0 2 4 6 8) (max-anagram n :predicate #'(lambda (ds) (= (rem (apply #'+ ds) 3) 0)))))
                  ((= 7 f) (find-multiple-7 n))
                  ((= 8 f) (cond
                             ((< n 10) (if (= (rem n 8) 0) n 0))
                             ((< n 100) (max-suffixes 2 *multiples-8-2* n))
                             (t (max-suffixes 3 *multiples-8-3* n))))
                  ((= 9 f) (max-anagram n :predicate #'(lambda (ds) (= (rem (apply #'+ ds) 9) 0))))
                  ((= 10 f) (max-suffixes 1 '(0) n))
                  )))
    (if (> result 0) result -1)))

(defun scan-input (line)
  (defun split-string (s)
    (loop for i = 0 then (1+ j)
          as j = (position #\Space s :start i)
          collect (subseq s i j)
          while j))
  (mapcar #'parse-integer (split-string line)))

(defun process-line (line)
  (let* ((input (scan-input line))
         (n (car input))
         (f (cadr input))
         (r (max-anagram-multiple f n)))
      (progn 
        (princ (max-anagram-multiple f n))
        (terpri))))

(defun process ()
  (handler-case
    (let ((line (read-line)))
      (process-line line)
      (process))
    (end-of-file () nil)))

(defun series ()
  (if (= (length *posix-argv*) 3)
    (let ((start (parse-integer (cadr *posix-argv*)))
          (end (parse-integer (caddr *posix-argv*))))
      (progn
        (loop for n from start to end
            do (loop for k from 1 to 10
                     do (format t "~A ~A:~A~%" n k (max-anagram-multiple k n))))
        (sb-ext:quit)))
    (progn
      (format  t "usage: sbcl --load series.lisp <start> <end>")
      (sb-ext:quit))))

(defun random- ()
  (if (= (length *posix-argv*) 2)
    (let ((counter (parse-integer (cadr *posix-argv*))))
      (let ((n (random (expt 10 1000))))
        (progn
          (loop for i from 1 to counter
                do (loop for k from 1 to 10
                         do (format t "~A ~A:~A~%" n k (max-anagram-multiple k n)))))
        (sb-ext:quit)))
    (progn
      (format t "usage: sbcl -- load random.lisp <count>")
      (sb-ext:quit))))
        
