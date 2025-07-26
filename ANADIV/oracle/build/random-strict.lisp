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
    (if (null d)
      0
      (+ (car d) (* 10 (to-number (cdr d))))))

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
    (let ((r (to-number (append ms (sort-all ss)))))
      (cond
        ((equal '(-1) ss) 0)
        ((and st (= r n))
         (let ((na (swap (to-swap (desc-prefix (sort-all ss))))))
           (if na
             (to-number (append ms na))
             0)))
        (t r)))))

; given a list of prefixes of a given size in digits, a number, and a strict boolean flag
; return the max anagram of all possible numbers with the same prefix as m
; e.g. (max-anagram-of-all '(0 2 4 6 8) 2 4807 nil) → 8740
; return the max anagram of 0 0 if list of multiples is empty
(defun max-anagram-of-all (ms s n st)
  (cond
    ((= s 0) (max-anagram-of 0 0 n st))
    (t (car
         (sort
           (mapcar #'(lambda (m) (max-anagram-of m s n st)) ms)
           #'>)))))

(defparameter *multiples-2* (loop for f from 0 to 4 collect (* f 2)))
(defparameter *multiples-5* (loop for f from 0 to 1 collect (* f 5)))
(defparameter *multiples-10* '(0))
(defparameter *multiples-4-1* (loop for f from 0 to 2 collect (* f 4)))
(defparameter *multiples-4-2* (loop for f from 0 to 24 collect (* f 4)))
(defparameter *multiples-8-1* (loop for f from 0 to 1 collect (* f 8)))
(defparameter *multiples-8-2* (loop for f from 0 to 12 collect (* f 8)))
(defparameter *multiples-8-3* (loop for f from 0 to 124 collect (* f 8)))

(defparameter *max-counter* 150)

(defun find-anagram-multiple-of-7 (n strict)

  (defun find-anagram-multiple-of-7-aux (na counter)
    (cond
      ((= counter 0) -1)
      ((= na 0) -1)
      ((and (= (rem na 7) 0) (or (not strict) (/= n na))) na)
      (t (find-anagram-multiple-of-7-aux
         (let ((na (swap (to-swap (desc-prefix (digits na))))))
           (if na (to-number na) 0))
         (- counter 1)))))
  (find-anagram-multiple-of-7-aux (to-number (sort-all (digits n))) *max-counter*))

(defun max-anagram-of (m s n st)
  (let* ((ms (digits m s))
         (ns (digits n))
         (ss (remove-digits ms ns)))
    (let ((r (to-number (append ms (sort-all ss)))))
      (cond
        ((equal '(-1) ss) 0)
        ((and st (= r n))
         (let ((na (swap (to-swap (desc-prefix (sort-all ss))))))
           (if na
             (to-number (append ms na))
             0)))
        (t r)))))
(defun max-anagram-multiple (f n &key strict)
  (let ((result (cond
                  ((= 1 f) (max-anagram-of 0 0 n strict))
                  ((= 2 f) (max-anagram-of-all *multiples-2* 1 n strict))
                  ((= 3 f) (if (= (rem (apply #'+ (digits n)) 3) 0)
                             (max-anagram-of 0 0 n strict)
                             0))
                  ((= 4 f) (cond
                             ((< n 10) (max-anagram-of-all *multiples-4-1* 1 n strict))
                             (t (max-anagram-of-all *multiples-4-2* 2 n strict))))
                  ((= 5 f) (max-anagram-of-all *multiples-5* 1 n strict))
                  ((= 6 f) (if (= (rem (apply #'+ (digits n)) 3) 0)
                             (max-anagram-of-all *multiples-2* 1 n strict)
                             0))
                  ((= 7 f) (find-anagram-multiple-of-7 n strict))
                  ((= 8 f) (cond
                             ((< n 10) (max-anagram-of-all *multiples-8-1* 1 n strict))
                             ((< n 100) (max-anagram-of-all *multiples-8-2* 2 n strict))
                             (t (max-anagram-of-all *multiples-8-3* 3 n strict))))
                  ((= 9 f) (if (= (rem (apply #'+ (digits n)) 9) 0)
                             (max-anagram-of 0 0 n strict)
                             0))
                  ((= 10 f) (max-anagram-of-all *multiples-10* 1 n strict))
                  )))
    (if (> result 0) result -1)))

(defun scan-input (line)
  (defun split-string (s)
    (loop for i = 0 then (1+ j)
          as j = (position #\Space s :start i)
          collect (subseq s i j)
          while j))
  (mapcar #'parse-integer (split-string line)))

(defun process-line (line &optional strict)
  (let* ((input (scan-input line))
         (n (car input))
         (f (cadr input))
         (r (max-anagram-multiple f n :strict strict)))
      (progn 
        (princ (max-anagram-multiple f n :strict strict))
        (terpri))))

(defun process (&optional strict)
  (handler-case
    (let ((line (read-line)))
      (process-line line strict)
      (process strict))
    (end-of-file () nil)))

(defun series (&optional strict)
  (if (= (length *posix-argv*) 3)
    (let ((start (parse-integer (cadr *posix-argv*)))
          (end (parse-integer (caddr *posix-argv*))))
      (progn
        (loop for n from start to end
            do (loop for k from 1 to 10
                     do (format t "~A ~A:~A~%" n k (max-anagram-multiple k n :strict strict))))
        (sb-ext:quit)))
    (progn
      (format  t "usage: sbcl --load series.lisp <start> <end>")
      (sb-ext:quit))))

(defun random- (&optional strict)
  (if (= (length *posix-argv*) 2)
    (let ((counter (parse-integer (cadr *posix-argv*))))
      (let ((n (random (expt 10 1000))))
        (progn
          (loop for i from 1 to counter
                do (loop for k from 1 to 10
                         do (format t "~A ~A:~A~%" n k (max-anagram-multiple k n :strict strict)))))
        (sb-ext:quit)))
    (progn
      (format t "usage: sbcl -- load random.lisp <count>")
      (sb-ext:quit))))
        
(random- t)
