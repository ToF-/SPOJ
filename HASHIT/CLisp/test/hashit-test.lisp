(require :asdf)
(require :lisp-unit)
(in-package :lisp-unit)
(setq *print-failures* t)
(load "src/hashit")

(define-test parsing-add-operation
    (let ((result (parse-operation (string "ADD:foo"))))
      (progn
            (assert-equal (string "foo") (cadr result))
            (assert-equal t (car result)))))

(define-test parsing-add-operation-for-any-key
    (let ((result (parse-operation (string "ADD:bar"))))
      (progn
            (assert-equal (string "bar") (cadr result))
            (assert-equal t (car result)))))

(define-test parsing-del-operation
    (let ((result (parse-operation (string "DEL:foo"))))
      (progn
            (assert-equal (string "foo") (cadr result))
            (assert-equal nil (car result)))))

(run-tests :all)
(sb-ext:quit)
