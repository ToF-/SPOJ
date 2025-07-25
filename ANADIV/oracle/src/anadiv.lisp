; anadiv.lisp


; returns the digits of a number in reverse order of rank
; e.g. (digits 4807) → '(7 0 8 4)
; given an optional size, pads digits with zeroes
; e.g. (digits 4807 7) → '(7 0 8 4 0 0 0)
; given a size of zero, gives an empty list
(defun digits (n &optional size)

  (defun digits-aux (n)
    (if (= n 0)
      ()
      (multiple-value-bind (q r)
        (truncate n 10)
        (cons r (digits-aux q)))))

  (let* ((r (if (= n 0) '(0) (digits-aux n)))
         (s (cond
              ((not size) 0)
              ((= size 0) nil)
              (t (- size (length r))))))
    (cond
      ((not s) ())
      (t (append r (loop repeat s collect 0))))))

; remove the digits in targets from the digit list
; e.g. (remove-digits '(4 7) '(4 8 0 7)) → '(8 0)
; e.g. (remove-digits '(4 9) '(4 8 0 7)) → nil
; e.g. (remove-digits '() '(4 8 0 7)) → '(4 8 0 7)
(defun remove-digits (tg l)
  (cond
    ((null tg) l)
    ((find (car tg) l) (remove-digits (cdr tg) (remove (car tg) l :count 1)))
    (t (list -1))))

; sort in ascending order a prefix of a list
; e.g. (sort-prefix '(7 0 8 4) 3) → '(0 7 8 4)
(defun sort-prefix (l n)
  (append (sort-all (subseq l 0 n))
          (subseq l n)))

(defun sort-all (l)
  (sort l #'<))


; isolate the longest descending prefix of a list
; e.g. (desc-prefix '(7 2 3 4) → '((7 2) 3 4)
(defun desc-prefix (l)

  (defun desc-prefix-aux (l p)
    (cond
      ((null l) (cons (reverse p) l))
      ((> (car l) (car p)) (cons (reverse p) l))
      (t  (desc-prefix-aux (cdr l) (cons (car l) p)))))

  (desc-prefix-aux (cdr l) (list (car l))))

; given a prefix and the rest of a list puts the digits to swap
; at front of prefix and rest respectively
; e.g. '(to-swap ((7 0) 4 8)) → '((0 7) 4 8) because 0 and 4 must be swapped 
(defun to-swap (sp)

  ; find in the ordered prefix the max digit < limit
  ; starts with pivot = first digit of prefix
  (defun find-pivot (prefix limit pivot result)
    (let ((c (car prefix))
          (cs (cdr prefix)))
    (cond
      ((null prefix) (cons pivot result))
      ((>= c limit) (find-pivot cs limit pivot (cons c result)))
      ((> c pivot) (find-pivot cs limit c (cons pivot result)))
      ((<= c pivot) (find-pivot cs limit pivot (cons c result))))))

  (if (null (cdr sp))
    nil
    (let ((prefix (sort-all (car sp)))
          (limit (cadr sp)))
      (cons
        (find-pivot (cdr prefix) limit (car prefix) ())
        (cdr sp)))))

; given an ordered prefix and the rest of a list swaps the first digit of each
; then reorder the prefix
; e.g. '(swap ((5 2 7) 6 8)) → '(2 6 7 5 8)
(defun swap (sp)
  (if (null sp)
    nil
    (let ((prefix (car sp))
          (suffix (cdr sp)))
      (append (sort-all (cons (car suffix) (cdr prefix)))
              (cons (car prefix) (cdr suffix))))))

; given a list of digits returns the number 
(defun to-number (d)
  (progn 
    (format t "(to-number ~A)~%" d)
    (if (null d)
      0
      (+ (car d) (* 10 (to-number (cdr d)))))))

; given a prefix of a given size in digits, a number, and a strict boolean flag
; returns the max anagram of n with the same prefix as m if no strict 
; returns the next anagram of n with the same prefix as m if strict
; returns 0 if no anagram of n can have the prefix
; returns 0 if n equals the only anagram with this prefix
; e.g. (max-anagram-of 24 2 4827 nil) → 8724
;      (max-anagram-of 7 2 8407 t)   → 4807
;      (max-anagram-of 3 1 4807 nil) → 0
;      (max-anagram-of 5 1 15 nil) → 15
;      (max-anagram-of 5 1 15 t) → 0
;      (max-anagram-of 0 0 4807 nil) → 8740
;      (max-anagram-of 0 0 8740 t) → 8704
(defun max-anagram-of (m s n st)
  (let* ((ms (digits m s))
         (ns (digits n))
         (ss (remove-digits ms ns)))
    (progn
      (format t "(max-anagram-of (~A ~A ~A ~A)~%" m s n st)
      (format t "ms:~A ns:~A ss:~A~%" ms ns ss)
      (let ((r (to-number (append ms (sort-all ss)))))
        (progn
          (format t "ms:~A (sort-all ss):~A r:~A~%" ms (sort-all ss) r)
          (cond
            ((equal '(-1) ss) 0)
            ((and st (= r n))
             (let ((na (swap (to-swap (desc-prefix (sort-all ss))))))
               (if na
                 (to-number (append ms na))
                 0)))
            (t r)))))))

(defun max-anagram (n &key (predicate #'(lambda (x) t)) strict)
  (let ((d (digits n)))
    (if (apply predicate (list d))
      (let ((result (to-number (sort-prefix d (length d)))))
        (if (and strict (= result n))
          (next-anagram result)
          result))
      0)))

(defun next-anagram (n)
  (to-number (swap (to-swap (desc-prefix (digits n))))))

(defun print-all-anagrams (n)
  (defun process (n)
    (if (= 0 n)
      ()
      (progn
        (format t "~A " n)
        (process (next-anagram n)))))
  (process (max-anagram n)))

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
      (t (let ((result (to-number (append tgt (sort-all prefix)))))
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
        
