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

(define-test not-finding-a-key-not-added
             (let ((h-table (make-h-table)))
               (assert-equal nil (find-key (string "foo") h-table))))

(define-test finding-an-added-key
    (let ((h-table (make-h-table))
          (key (string "e")))
      (progn
        (add-key key h-table)
        (assert-equal 0 (find-key key h-table)))))

(define-test finding-any-added-key
    (let ((h-table (make-h-table))
          (key (string "f")))
      (progn
        (add-key key h-table)
        (assert-equal 19 (find-key key h-table)))))

(define-test finding-a-key-with-same-hash
    (let ((h-table (make-h-table))
          (key-e (string "e"))
          (key-ee (string "ee")))
      (progn
        (add-key key-e h-table)
        (add-key key-ee h-table)
        (assert-equal 0 (find-key key-e h-table))
        (assert-equal 24 (find-key key-ee h-table)))))

(define-test deleting-a-key
    (let ((h-table (make-h-table))
          (key (string "foo")))
      (progn
        (add-key key h-table)
        (assert-equal 60 (find-key key h-table))
        (delete-key key h-table)
        (assert-equal nil (find-key key h-table)))))

(define-test number-of-keys
    (let ((h-table (make-h-table)))
      (progn
        (add-key (string "foo") h-table)
        (add-key (string "bar") h-table)
        (add-key (string "qux") h-table)
        (delete-key (string "foo") h-table)
        (assert-equal 2 (nb-keys h-table)))))

(define-test cannot-add-an-existing-key
    (let ((h-table (make-h-table)))
      (progn
        (add-key (string "foo") h-table)
        (add-key (string "foo") h-table)
        (assert-equal 1 (nb-keys h-table)))))

(define-test cannot-delete-an-non-existing-key
    (let ((h-table (make-h-table)))
      (progn
        (add-key (string "e") h-table)
        (delete-key (string "ee") h-table)
        (assert-equal 1 (nb-keys h-table)))))

(run-tests :all)
(sb-ext:quit)
