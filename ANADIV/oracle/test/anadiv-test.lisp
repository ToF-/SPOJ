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

(define-test digits-within-size
             (assert-equal '(0 0 0) (digits 0 3))
             (assert-equal '(8 0 0 0) (digits 8 4))
             (assert-equal '(6 9) (digits 96 1))
             (assert-equal '(0 4) (digits 40 2))
             )

(define-test remove-digits
             (assert-equal '(0 7) (remove-digits '(4 8) '(4 8 0 7)))
             )

(define-test remove-digits-missing-digit
             (assert-equal '(-1) (remove-digits '(4 9) '(4 8 0 7)))
             )

(define-test remove-digits-no-digits-to-remove
             (assert-equal '(4 8 0 7) (remove-digits '() '(4 8 0 7)))
             )

(define-test sort-prefix
             (assert-equal '(4 8 0 7) (sort-prefix '(8 4 0 7) 2))
             (assert-equal '(0 2 3 1 9) (sort-prefix '(2 0 3 1 9) 3))
             )

(define-test sort-all
             (assert-equal '(0 4 7 8) (sort-all '(4 8 0 7)))
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

(define-test max-anagram-of-no-prefix-no-strict
             (assert-equal 5 (max-anagram-of 0 0 5 nil))
             (assert-equal 8740 (max-anagram-of 0 0 4807 nil))
             )

(define-test max-anagram-of-no-prefix-strict
             (assert-equal 0 (max-anagram-of 0 0 5 t))
             (assert-equal 8704 (max-anagram-of 0 0 8740 t))
             )

(define-test max-anagram-of-single-digit-prefix-no-strict
             (assert-equal 28 (max-anagram-of 8 1 28 nil))
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
             (assert-equal 8 (max-suffixes 1 '(0 2 4 6 8) 8))
             )
(define-test max-suffixes-mising-digits
             (assert-equal 0 (max-suffixes 1 '(0 2 4 6 8) 13579))
             )

(define-test max-anagram-multiple-of-1
             (assert-equal 8740 (max-anagram-multiple 1 4807))
             (assert-equal 9 (max-anagram-multiple 1 9))
             )

(define-test max-anagram-multiple-of-1-strict
             (assert-equal 8704 (max-anagram-multiple 1 8740 :strict t))
             (assert-equal -1 (max-anagram-multiple 1 9 :strict t))
             )
(define-test max-anagram-multiple-of-2
             (assert-equal 8740 (max-anagram-multiple 2 4807))
             (assert-equal 099888773210(max-anagram-multiple 2 38709871298))
             (assert-equal -1 (max-anagram-multiple 2 173799955533311133))
             (assert-equal 8 (max-anagram-multiple 2 8))
             )

(define-test max-anagram-multiple-of-2-strict
             (assert-equal -1 (max-anagram-multiple 2 8 :strict t))
             (assert-equal 28 (max-anagram-multiple 2 82 :strict t))
             (assert-equal 68 (max-anagram-multiple 2 86 :strict t))
             (assert-equal 418  (max-anagram-multiple 2 814 :strict t))
             (assert-equal 8704 (max-anagram-multiple 2 8740 :strict t))
             )
(define-test max-anagram-multiple-of-3
             (assert-equal 95433 (max-anagram-multiple 3 93543))
             (assert-equal -1 (max-anagram-multiple 3 374))
             )

(define-test max-anagram-multiple-of-3-strict
             (assert-equal -1 (max-anagram-multiple 3 9 :strict t))
             (assert-equal 95343 (max-anagram-multiple 3 95433 :strict t))
             )

(define-test max-anagram-multiple-of-4
             (assert-equal 8 (max-anagram-multiple 4 8))
             (assert-equal 8740 (max-anagram-multiple 4 4807))
             (assert-equal 7184 (max-anagram-multiple 4 4817))
             (assert-equal 988888777743333120 (max-anagram-multiple 4 387908731873874328))
             (assert-equal -1 (max-anagram-multiple 4 333))
             )

(define-test max-anagram-multiple-of-4-strict
             (assert-equal -1 (max-anagram-multiple 4 8 :strict t))
             (assert-equal 8704 (max-anagram-multiple 4 8740 :strict t))
             )

(define-test max-anagram-multiple-of-5
             (assert-equal 5 (max-anagram-multiple 5 5))
             (assert-equal 988888777743333210 (max-anagram-multiple 5 387908731873874328))
             (assert-equal -1 (max-anagram-multiple 5 387481))
             )

(define-test max-anagram-multiple-of-5-strict
             (assert-equal -1 (max-anagram-multiple 5 5 :strict t))
             (assert-equal -1 (max-anagram-multiple 5 55555 :strict t))
             (assert-equal 98670 (max-anagram-multiple 5 98760 :strict t))
             )

(define-test max-anagram-multiple-of-6
             (assert-equal 6 (max-anagram-multiple 6 6))
             (assert-equal 12 (max-anagram-multiple 6 21))
             (assert-equal 738 (max-anagram-multiple 6 783))
             (assert-equal -1 (max-anagram-multiple 6 387481))
             )

(define-test max-anagram-multiple-of-7
            (assert-equal 7 (max-anagram-multiple 7 7))
            (assert-equal -1 (max-anagram-multiple 7 131))
            (assert-equal 971614 (max-anagram-multiple 7 971614))
            (assert-equal 976115 (max-anagram-multiple 7 971615))
            )

(define-test max-anagram-multiple-of-8
             (assert-equal 8 (max-anagram-multiple 8 8))
             (assert-equal -1 (max-anagram-multiple 8 131))
             (assert-equal 32 (max-anagram-multiple 8 23))
             (assert-equal 877432112 (max-anagram-multiple 8 211234778))
             (assert-equal 777432112(max-anagram-multiple 8 211234777))
             )

(define-test max-anagram-multiple-of-9
             (assert-equal 9 (max-anagram-multiple 9 9))
             (assert-equal 9775443333321 (max-anagram-multiple 9 3337354374921))
             (assert-equal 9775443321000 (max-anagram-multiple 9 3007354074921))
             )

(define-test max-anagram-multiple-of-10
             (assert-equal -1 (max-anagram-multiple 10 8))
             (assert-equal 8740 (max-anagram-multiple 10 4807))
             (assert-equal -1 (max-anagram-multiple 10 4887))
             )

(define-test scan-input
             (assert-equal '(4807 7) (scan-input "4807 7"))
             (assert-equal '(380989734122308974510398734551098734512309875109875123 5)
                           (scan-input "380989734122308974510398734551098734512309875109875123 5"))
             )

(define-test process-line
             (assert-equal nil (process-line "4807 3")) ; should print -1
             (assert-equal nil (process-line "4807 7")) ; shourd print 8470
             )
; (run-tests :all)
(run-tests '(digits
              digits-within-size
              remove-digits
              remove-digits-missing-digit
              remove-digits-no-digits-to-remove
              sort-prefix
              sort-all
              desc-prefix-full-length
              desc-prefix-partial-length
              to-swap
              to-swap-single-pivot
              to-swap-full-length-prefix
              swap
              swap-nil
              number-
              max-anagram-of-no-prefix-no-strict
              max-anagram-of-no-prefix-strict
              max-anagram-of-single-digit-prefix-no-strict
              ))
(sb-ext:quit)
