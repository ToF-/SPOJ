(require :asdf)
(require :lisp-unit)
(in-package :lisp-unit)
(setq *print-failures* t)
(load "src/anadiv")

(define-test number-pair-from-string
             (assert-equal (cons 4807 7) (number-pair-from-string "4807 7"))
             )

(define-test digits
             (assert-equal '(0 4 7 8) (digits 4807))
             (assert-equal '(2 4 5 9 9) (digits 54929))
             )

(define-test digits-to-number
             (assert-equal 4807 (digits-to-number '(4 8 0 7)))
             )

(define-test number-to-digits
             (assert-equal '(4 8 0 7) (number-to-digits 4807))
             )

(define-test max-anagram
             (assert-equal '(8 7 4 0) (max-anagram '(4 8 0 7)))
             )
(define-test split-digits
             (assert-equal '((1)) (split-digits '(1)))
             (assert-equal '((1 3)) (split-digits '(1 3)))
             (assert-equal '((0 4 7 8)) (split-digits '(0 4 7 8)))
             (assert-equal '((7) 2) (split-digits '(7 2)))
             (assert-equal '((7 9) 2 5 3) (split-digits '(7 9 2 5 3)))
             (assert-equal '((4 8) 0 7) (split-digits '(4 8 0 7)))
             (assert-equal '((3) 2 7 6 8) (split-digits '(3 2 7 6 8)))
             (assert-equal '((9) 8 6 5 1) (split-digits '(9 8 6 5 1)))
             )

(define-test extract-lower
             (assert-equal '(4 0 7 8) (extract-lower 7 '(4 8 0 7)))
             (assert-equal '(7 4 0 8) (extract-lower 8 '(4 8 0 7)))
             (assert-equal '(0 4 7 8) (extract-lower 4 '(4 8 0 7)))
             (assert-equal '(8 7 4 0) (extract-lower 9 '(4 8 0 7)))
             )

(define-test max-lower-anagram
             (assert-equal '() (max-lower-anagram '(1)))
             (assert-equal '() (max-lower-anagram '(1 2 5 7 8 9)))
             (assert-equal '(1 4 2 3) (max-lower-anagram '(1 4 3 2)))
             (assert-equal '(1 2) (max-lower-anagram '(2 1)))
             (assert-equal '(2 4) (max-lower-anagram '(4 2)))
             (assert-equal '(1 3 5 4 2) (max-lower-anagram '(1 4 2 3 5)))
             (assert-equal '(3 5 4 2) (max-lower-anagram '(4 2 3 5)))
             (assert-equal '() (max-lower-anagram '(2 3 5)))
             (assert-equal '(4 1 5 2) (max-lower-anagram '(4 2 1 5)))
             )

(define-test digit-subtract
             (assert-equal '(5) (digit-subtract '(7) '(2)))
             (assert-equal '(1 5) (digit-subtract '(1 7) '(2)))
             (assert-equal '(1 2 3) (digit-subtract '(1 3 4) '(1 1)))
             (assert-equal '(1 2 3 4) (digit-subtract '(1 4 5 6) '(2 2 2)))
             (assert-equal '(2 3) (digit-subtract '(3 1) '(8)))
             (assert-equal '(9 8 9)    (digit-subtract '(1 0 0 6) '(1 7)))
             )

(define-test divisible
             (assert-equal t (divisible (number-to-digits 4807) 1))
             (assert-equal nil (divisible (number-to-digits 4807) 2))
             (assert-equal t (divisible (number-to-digits 32768) 2))
             (assert-equal nil (divisible (number-to-digits 4807) 3))
             (assert-equal t (divisible (number-to-digits (* 3 4807)) 3))
             (assert-equal nil (divisible (number-to-digits 4807) 4))
             (assert-equal t (divisible (number-to-digits 4848) 4))
             (assert-equal nil (divisible (number-to-digits 4807) 5))
             (assert-equal t (divisible (number-to-digits 4810) 5))
             (assert-equal nil (divisible (number-to-digits 4807) 6))
             (assert-equal t (divisible (number-to-digits 4806) 6))
             )
(run-tests :all)
(sb-ext:quit)
