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
             (assert-equal '((1)) (split-digits '(1)))
             (assert-equal '((1 3)) (split-digits '(1 3)))
             (assert-equal '((0 4 7 8)) (split-digits '(0 4 7 8)))
             (assert-equal '((7) 2) (split-digits '(7 2)))
             (assert-equal '((7 9) 2 5 3) (split-digits '(7 9 2 5 3)))
             (assert-equal '((4 8) 0 7) (split-digits '(4 8 0 7)))
             (assert-equal '((3) 2 7 6 8) (split-digits '(3 2 7 6 8)))
             (assert-equal '((9) 8 6 5 1) (split-digits '(9 8 6 5 1)))
             )

(define-test max-lower-anagram
             (assert-equal '() (max-lower-anagram '(1)))
             (assert-equal '() (max-lower-anagram '(1 2 5 7 8 9)))
             (assert-equal '(1 2) (max-lower-anagram '(2 1)))
             (assert-equal '(2 4) (max-lower-anagram '(4 2)))
             (assert-equal '(2 5 4) (max-lower-anagram '(4 2 5)))
             (assert-equal '(4 1 5 2) (max-lower-anagram '(4 2 1 5)))
             )

(run-tests :all)
(sb-ext:quit)
