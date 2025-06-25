(require :asdf)
(require :lisp-unit)
(in-package :lisp-unit)
(setq *print-failures* t)
(load "src/anadiv")

(define-test digits
             (assert-equal '(8 7 4 0) (digits 4807))
             (assert-equal '(9 9 5 4 2) (digits 54929))
             )

(define-test extract-element
             (assert-equal '(7 1) (extract 0 '(7 1)))
             (assert-equal '(1 7) (extract 1 '(7 1)))
             (assert-equal '(3 1 2) (extract 2 '(1 2 3)))
             )

(define-test extractions
             (assert-equal '((1 2 3)(2 1 3)(3 1 2)) (extractions '(1 2 3)))
             )

(run-tests :all)
(sb-ext:quit)
