(require :asdf)
(require :lisp-unit)
(in-package :lisp-unit)
(setq *print-failures* t)
(load "src/crypto1")

(define-test power
    (progn
      (assert-equal 1 (power 4 0 9))
      (assert-equal 4 (power 4 1 9))
      (assert-equal 16 (power 4 2 100))
      (assert-equal (mod (power 4 2 100) 9) (power 4 2 9))
      (assert-equal (* 4 4 4) (power 4 3 100))
      (assert-equal 384 (power 4 4807 10000))
      (assert-equal 2912856368 (power 1749870067 (1+ 1000000001) 4000000007))
      ))
    
(define-test target-time
    (assert-equal "Sun Jun 13 16:20:39 2004" (target-time 1749870067)))

(run-tests :all)
(sb-ext:quit)
