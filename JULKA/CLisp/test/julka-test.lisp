(require :asdf)
(require :lisp-unit)
(in-package :lisp-unit)
(setq *print-failures* t)
(load "src/julka")

(define-test computing-apples
    (let ((result (compute-apples 10 2)))
      (progn 
        (assert-equal 6 (car result))
        (assert-equal 4 (cadr result)))))

(run-tests :all)
(sb-ext:quit)
