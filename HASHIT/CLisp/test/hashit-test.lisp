(require :asdf)
(require :lisp-unit)
(in-package :lisp-unit)
(setq *print-failures* t)
(load "src/hashit")

(define-test parsing-operation
    (let ((result (parse-operation (string "ADD:foo"))))
          (prog
            (assert-equal ('add-key (car result)))
            (assert-equal ((string "foo") (cadr result))))))


(run-tests :all)
(sb-ext:quit)
