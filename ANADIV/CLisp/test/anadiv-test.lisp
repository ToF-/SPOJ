(require :asdf)
(require :lisp-unit)
(in-package :lisp-unit)
(setq *print-failures* t)
(load "src/anadiv")

(define-test number-pair-from-string
             (assert-equal (cons 4807 7) (number-pair-from-string "4807 7"))
             )

(define-test digits-from-number
             (assert-equal (list 3) (digits-from-number 3))
             (assert-equal (list 7) (digits-from-number 7))
             (assert-equal (reverse (list 4 2)) (digits-from-number 42))
             (assert-equal (reverse (list 1 7)) (digits-from-number 17))
             (assert-equal (reverse (list 4 8 0 7)) (digits-from-number 4807))
             )

(define-test value
             (assert-equal 4807 (value (list 7 0 8 4)))
             (assert-equal 42 (value (list 2 4 0 0 0)))
             )

(define-test subtract
             (defun assert-subtract (result minuend subtrahend)
               (assert-equal result
                             (value (subtract
                                      (digits-from-number minuend)
                                      (digits-from-number subtrahend)))))

             (assert-subtract 7 14 7)
             (assert-subtract 3 7 4)
             (assert-subtract 15 17 2)
             (assert-subtract 32 34 2)
             (assert-subtract 24 32 8)
             (assert-subtract 99 100 1)
             (assert-subtract 75 100 25)
             )

(define-test multiple
             (assert-equal t (multiple (digits-from-number 7) 7))
             (assert-equal nil (multiple (digits-from-number 8) 7))
             (assert-equal t (multiple (digits-from-number (* 7 7 7)) 7))
             (assert-equal nil (multiple (digits-from-number 22) 7))
             )

(define-test add
             (defun assert-add (result operand addend)
               (assert-equal result
                             (value (add
                                      (digits-from-number operand)
                                      (digits-from-number addend)))))

             (assert-add 7 3 4)
             (assert-add 9 5 4)
             (assert-add 10 5 5)
             (assert-add 243 210 33)
             )

(define-test sum-digits
             (defun assert-sum-digits (result operand)
               (assert-equal result (value (sum-digits (digits-from-number operand)))))

             (assert-sum-digits 19 4807)
             (assert-sum-digits 22 (* 7 7 7 7 7))
             )

(define-test divisible-by-7
             (assert-equal t (divisible-by 7 (digits-from-number 7)))
             (assert-equal nil (divisible-by 7 (digits-from-number 6)))
             (assert-equal t (divisible-by 7 (digits-from-number (* 7 7 7 7 7 7 7 7 7 7 7))))
             )

(define-test divisible-by-8
             (assert-equal t (divisible-by 8 (digits-from-number 8)))
             (assert-equal nil (divisible-by 8 (digits-from-number 7)))
             (assert-equal t (divisible-by 8 (digits-from-number 256)))
             (assert-equal t (divisible-by 8 (digits-from-number (* 8 8 8 8 8 8 8 8 8 8 8 8 8))))
             (assert-equal nil (divisible-by 8 (digits-from-number (1+ (* 8 8 8 8 8 8 8 8 8 8 8)))))
             )

(define-test divisible-by-9
             (assert-equal t (divisible-by 9 (digits-from-number (* 9 9 9 9 9 9 9 9 9 9 9))))
             (assert-equal t (divisible-by 9 (digits-from-number (* 9 9 9 9 9 9 9 9 9 9 9))))
             )

(define-test divisible-by-3
             (assert-equal t (divisible-by 3 (digits-from-number (* 3 3 3 3 3 3 3 3 3 3))))
             (assert-equal nil (divisible-by 3 (digits-from-number (1+ (* 3 3 3 3 3 3 3 3 3 3)))))
             )

(define-test divisible-by-4
             (assert-equal t (divisible-by 4 (digits-from-number 12)))
             (assert-equal t (divisible-by 4 (digits-from-number (* 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4))))
             (assert-equal nil (divisible-by 4 (digits-from-number (1+ (* 4 4 4 4 4 4 4 4 4 4 4 4 4 4 4)))))
             )

(define-test divisible-by-5
             (assert-equal t (divisible-by 5 (digits-from-number 120)))
             (assert-equal t (divisible-by 5 (digits-from-number (* 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5))))
             (assert-equal nil (divisible-by 5 (digits-from-number (1+ (* 5 5 5 5 5 5 5 5 5 5 5 5 5 5 5)))))
             )

(define-test divisible-by-2
             (assert-equal t (divisible-by 2 (digits-from-number 120)))
             (assert-equal t (divisible-by 2 (digits-from-number (* 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2))))
             (assert-equal nil (divisible-by 2 (digits-from-number (1+ (* 2 2 2 2 2 2 2 2 2 2 2 2 2 2 2)))))
             )
(define-test divisible-by-10
             (assert-equal t (divisible-by 10 (digits-from-number 120)))
             (assert-equal t (divisible-by 10 (digits-from-number (* 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10))))
             (assert-equal nil (divisible-by 10 (digits-from-number (1+ (* 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10)))))
             )
(define-test divisible-by-6
             (assert-equal t (divisible-by 6 (digits-from-number 6)))
             (assert-equal t (divisible-by 6 (digits-from-number (* 6 6 6 6 6 6 6 6 6 6))))
             (assert-equal nil (divisible-by 6 (digits-from-number (1+ (* 6 6 6 6 6 6 6 6 6 6)))))
             )

(define-test max-anagram
             (assert-equal (list 8 7 4 0) (max-anagram (digits-from-number 4807)))
             )

(define-test next-anagram
             (assert-equal nil (next-anagram (digits-from-number 4)))
             (assert-equal 14 (value (next-anagram (digits-from-number 41))))
             (assert-equal 27 (value (next-anagram (digits-from-number 72))))
             (assert-equal nil (next-anagram (digits-from-number 14)))
             )

(run-tests :all)
(sb-ext:quit)
