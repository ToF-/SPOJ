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
             )

(define-test divisible-by-7
             (assert-equal t (divisible-by-7 (digits-from-number 7)))
             (assert-equal nil (divisible-by-7 (digits-from-number 6)))
             (assert-equal t (divisible-by-7 (digits-from-number (* 7 7 7 7 7 7 7 7 7 7 7))))
             )
; 
; 
; (define-test digits
;              (assert-equal '(0 4 7 8) (digits 4807))
;              (assert-equal '(2 4 5 9 9) (digits 54929))
;              )
; 
; (define-test digits-to-number
;              (assert-equal 4807 (digits-to-number '(4 8 0 7)))
;              )
; 
; (define-test number-to-digits
;              (assert-equal '(4 8 0 7) (number-to-digits 4807))
;              )
; 
; (define-test max-anagram
;              (assert-equal '(8 7 4 0) (max-anagram '(4 8 0 7)))
;              )
; (define-test split-digits
;              (assert-equal '((1)) (split-digits '(1)))
;              (assert-equal '((1 3)) (split-digits '(1 3)))
;              (assert-equal '((0 4 7 8)) (split-digits '(0 4 7 8)))
;              (assert-equal '((7) 2) (split-digits '(7 2)))
;              (assert-equal '((7 9) 2 5 3) (split-digits '(7 9 2 5 3)))
;              (assert-equal '((4 8) 0 7) (split-digits '(4 8 0 7)))
;              (assert-equal '((3) 2 7 6 8) (split-digits '(3 2 7 6 8)))
;              (assert-equal '((9) 8 6 5 1) (split-digits '(9 8 6 5 1)))
;              )
; 
; (define-test extract-lower
;              (assert-equal '(4 0 7 8) (extract-lower 7 '(4 8 0 7)))
;              (assert-equal '(7 4 0 8) (extract-lower 8 '(4 8 0 7)))
;              (assert-equal '(0 4 7 8) (extract-lower 4 '(4 8 0 7)))
;              (assert-equal '(8 7 4 0) (extract-lower 9 '(4 8 0 7)))
;              )
; 
; (define-test max-lower-anagram
;              (assert-equal '() (max-lower-anagram '(1)))
;              (assert-equal '() (max-lower-anagram '(1 2 5 7 8 9)))
;              (assert-equal '(1 4 2 3) (max-lower-anagram '(1 4 3 2)))
;              (assert-equal '(1 2) (max-lower-anagram '(2 1)))
;              (assert-equal '(2 4) (max-lower-anagram '(4 2)))
;              (assert-equal '(1 3 5 4 2) (max-lower-anagram '(1 4 2 3 5)))
;              (assert-equal '(3 5 4 2) (max-lower-anagram '(4 2 3 5)))
;              (assert-equal '() (max-lower-anagram '(2 3 5)))
;              (assert-equal '(4 1 5 2) (max-lower-anagram '(4 2 1 5)))
;              )
; 
; (define-test digit-subtract
;              (assert-equal '(5) (digit-subtract '(7) '(2)))
;              (assert-equal '(1 5) (digit-subtract '(1 7) '(2)))
;              (assert-equal '(1 2 3) (digit-subtract '(1 3 4) '(1 1)))
;              (assert-equal '(1 2 3 4) (digit-subtract '(1 4 5 6) '(2 2 2)))
;              (assert-equal '(2 3) (digit-subtract '(3 1) '(8)))
;              (assert-equal '(0 9 8 9)    (digit-subtract '(1 0 0 6) '(1 7)))
;              )
; 
; (define-test divisible
;              (assert-equal t (divisible (number-to-digits 4807) 1))
;              (assert-equal nil (divisible (number-to-digits 4807) 2))
;              (assert-equal t (divisible (number-to-digits 32768) 2))
;              (assert-equal nil (divisible (number-to-digits 4807) 3))
;              (assert-equal t (divisible (number-to-digits (* 3 4807)) 3))
;              (assert-equal nil (divisible (number-to-digits 4807) 4))
;              (assert-equal t (divisible (number-to-digits 4848) 4))
;              (assert-equal nil (divisible (number-to-digits 4807) 5))
;              (assert-equal t (divisible (number-to-digits 4810) 5))
;              (assert-equal nil (divisible (number-to-digits 4807) 6))
;              (assert-equal t (divisible (number-to-digits 4806) 6))
;              (assert-equal t (divisible (number-to-digits (* 14 14 4807)) 7))
;              (assert-equal nil (divisible (number-to-digits 4807) 7))
;              (assert-equal t (divisible (number-to-digits 4862) 8))
;              (assert-equal nil (divisible (number-to-digits 4807) 8))
;              (assert-equal nil (divisible (number-to-digits 4807) 9))
;              (assert-equal t (divisible (number-to-digits 4806) 9))
;              (assert-equal nil (divisible (number-to-digits 4807) 10))
;              (assert-equal t (divisible (number-to-digits 4810) 10))
;              )
(run-tests :all)
(sb-ext:quit)
