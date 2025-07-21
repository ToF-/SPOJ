; anadiv-test.lisp
(require :asdf)
(require :lisp-unit)
(in-package :lisp-unit)
(setq *print-failures* t)
(setq *print-errors* t)
(load "src/anadiv")


(define-test max-subsequence-on-full-length
             (assert-equal '(0 4 7 8) (max-subsequence '(4 8 0 7) 4))
             )

(define-test max-subsequence-on-partial-length
             (assert-equal '(4 8 0 7) (max-subsequence '(8 4 0 7) 2))
             (assert-equal '(0 2 3 1 9) (max-subsequence '(2 0 3 1 9) 3))
             )

(define-test descending-prefix-full-length
             (assert-equal '((8 7 4 0)) (descending-prefix '(8 7 4 0)))
             )

(define-test descending-prefix-partial-length
             (assert-equal '((7 2) 3 4) (descending-prefix '(7 2 3 4)))
             (assert-equal '((7 5 3) 4) (descending-prefix '(7 5 3 4)))
             (assert-equal '((1) 5 3 4) (descending-prefix '(1 5 3 4)))
             )

(define-test swap-digits-and-reorder
             (assert-equal '(7 0 4 8) (swap-digits-and-reorder '((4 0) 7 8)))
             )

(run-tests :all)
(sb-ext:quit)
