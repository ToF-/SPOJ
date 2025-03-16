(require :asdf)
(require :lisp-unit)
(in-package :lisp-unit)
(setq *print-failures* t)
(load "src/hashit")

(define-test parsing-operation
    (let ((result (parse-operation (string "ADD:foo"))))
      (progn
            (assert-equal (string "foo") (cadr result))
            (assert-equal t (car result)))))


(run-tests :all)
(sb-ext:quit)
