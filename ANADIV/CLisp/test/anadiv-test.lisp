(require :asdf)
(require :lisp-unit)
(in-package :lisp-unit)
(setq *print-failures* t)
(load "src/anadiv")

(define-test number-pair-from-string
             (assert-equal (cons 4807 7) (number-pair-from-string "4807 7"))
             )

(define-test digits
             (assert-equal '(0 4 7 8) (digits 4807))
             (assert-equal '(2 4 5 9 9) (digits 54929))
             )

(define-test split-digits
             (assert-equal '((1) ()) (split-digits '(1)))
             (assert-equal '((4 8) (0 7)) (split-digits '(4 8 0 7)))
             (assert-equal '((3) (2 7 6 8)) (split-digits '(3 2 7 6 8)))
             (assert-equal '((9) (8 6 5 1)) (split-digits '(9 8 6 5 1)))
             )

(define-test max-lower-anagram
             (assert-equal '() (max-lower-anagram '(0 4 7 8)))
             (assert-equal '(0 4 7 8) (max-lower-anagram '(0 4 8 7)))
             (assert-equal '(4 7 8 0) (max-lower-anagram '(4 8 0 7)))
             (assert-equal '(4 7 0 8) (max-lower-anagram '(4 7 8 0)))
             (assert-equal '(4 0 8 7) (max-lower-anagram '(4 7 0 8)))
             )

(define-test number-as-list
             (assert-equal '(7 0 8 4) (nal 4807))
             (assert-equal '(1 8 2) (nal 281))
             )

(define-test list-as-number
             (assert-equal 7 (lan '(7)))
             (assert-equal 217 (lan '(7 1 2)))
             (assert-equal 7084 (lan '(4 8 0 7)))
             (let ((n (random 100000)))
                 (assert-equal n (lan (nal n))))
             )

(define-test number-as-list-9-complement
             (assert-equal 8 (lan (nal-9-complement (nal 1))))
             (assert-equal 5192 (lan (nal-9-complement (nal 4807))))
             )

(define-test number-as-list-minus
             (assert-equal 4 (lan (nal-minus (nal 7) (nal 3))))
             (assert-equal 15 (lan (nal-minus (nal 18) (nal 3))))
             (assert-equal 17 (lan (nal-minus (nal 22) (nal 5))))
             )

(define-test all-permutations
             (assert-equal '(()) (all-permutations '()))
             (assert-equal '((1)) (all-permutations '(1)))
             (assert-equal '((1 2) (2 1)) (all-permutations '(1 2)))
             (assert-equal '((1 2 3) (1 3 2) (2 1 3) (2 3 1) (3 1 2) (3 2 1)) (all-permutations '(1 2 3)))
             )


(run-tests :all)
(sb-ext:quit)
