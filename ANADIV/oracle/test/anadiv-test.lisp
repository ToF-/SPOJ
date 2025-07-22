; anadiv-test.lisp
(require :asdf)
(require :lisp-unit)
(in-package :lisp-unit)
(setq *print-failures* t)
(setq *print-errors* t)
(load "src/anadiv")


(define-test digits
              (assert-equal '(7 0 8 4) (digits 4807))
              (assert-equal '(1) (digits 1))
              (assert-equal '(0) (digits 0))
              )

(define-test max-subseq-on-full-length
             (assert-equal '(0 4 7 8) (max-subseq '(4 8 0 7) 4))
             )

(define-test max-subseq-on-partial-length
             (assert-equal '(4 8 0 7) (max-subseq '(8 4 0 7) 2))
             (assert-equal '(0 2 3 1 9) (max-subseq '(2 0 3 1 9) 3))
             )

(define-test desc-prefix-full-length
             (assert-equal '((8 7 4 0)) (desc-prefix '(8 7 4 0)))
             )

(define-test desc-prefix-partial-length
             (assert-equal '((7 2) 3 4) (desc-prefix '(7 2 3 4)))
             (assert-equal '((7 5 3) 4) (desc-prefix '(7 5 3 4)))
             (assert-equal '((1) 5 3 4) (desc-prefix '(1 5 3 4)))
             )

(define-test to-swap
             (assert-equal '((7 0) 8 4) (to-swap '((0 7) 8 4)))
             (assert-equal '((7 0) 9 7) (to-swap '((0 7) 9 7)))
             (assert-equal '((5 3 1) 8 4) (to-swap '((1 3 5) 8 4)))
             )

(define-test to-swap-single-pivot
             (assert-equal '((7) 8 0 4) (to-swap '((7) 8 0 4)))
             )

(define-test to-swap-full-length-prefix
             (assert-equal nil (to-swap '((0 4 7 8))))
             )

(define-test swap
             (assert-equal '(0 8 7 4) (swap '((7 0) 8 4)))
             (assert-equal '(8 7 0 4) (swap '((7) 8 0 4)))
             )

(define-test swap-nil
             (assert-equal nil (swap nil))
             )

(define-test number-
             (assert-equal 4807 (number- '(7 0 8 4)))
             )

(define-test max-anagram
             (assert-equal 98740 (max-anagram 48097))
             )

(define-test next-anagram
             (assert-equal 4780 (next-anagram 4807))
             (assert-equal 4708 (next-anagram 4780))
             (assert-equal 4087 (next-anagram 4708))
             (assert-equal 212 (next-anagram 221))
             )

(define-test next-anagram-last-anagram
             (assert-equal 0 (next-anagram 123456789))
             (assert-equal 0 (next-anagram 478))
             )

(define-test remove-digits
             (assert-equal '(0 7) (remove-digits '(4 8) '(4 8 0 7)))
             )

(define-test remove-digits-missing-digit
             (assert-equal nil (remove-digits '(4 9) '(4 8 0 7)))
             )
(define-test max-suffix
             (assert-equal 7048 (max-suffix 2 48 4807))
             (assert-equal 70048 (max-suffix 3 48 48007))
             (assert-equal 87640351 (max-suffix 3 351 56148307))
             )

(define-test max-suffix-missing-digits
             (assert-equal 0 (max-suffix 3 48 4817))
             )
(define-test max-suffixes
             (assert-equal 8740 (max-suffixes 1 '(0 2 4 6 8) 4807))
             (assert-equal 874 (max-suffixes 1 '(0 2 4 6 8) 487))
             (assert-equal 86 (max-suffixes 1 '(0 2 4 6 8) 68))
             (assert-equal 9877263 (max-suffixes 2 '(0 12 42 63 87) 6798732))
             )
(define-test max-suffixes-mising-digits
             (assert-equal 0 (max-suffixes 1 '(0 2 4 6 8) 13579))
             )

(run-tests :all)
(sb-ext:quit)
