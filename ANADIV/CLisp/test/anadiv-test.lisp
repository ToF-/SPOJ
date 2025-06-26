(require :asdf)
(require :lisp-unit)
(in-package :lisp-unit)
(setq *print-failures* t)
(load "src/anadiv")

(define-test digits
             (assert-equal '(8 7 4 0) (digits 4807))
             (assert-equal '(9 9 5 4 2) (digits 54929))
             )

(define-test all-permutations
             (assert-equal '(()) (all-permutations '()))
             (assert-equal '((1)) (all-permutations '(1)))
             (assert-equal '((1 2) (2 1)) (all-permutations '(1 2)))
             (assert-equal '((1 2 3) (1 3 2) (2 1 3) (2 3 1) (3 1 2) (3 2 1)) (all-permutations '(1 2 3)))
             )

(run-tests :all)
(sb-ext:quit)
