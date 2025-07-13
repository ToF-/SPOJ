(require :asdf)
(require :lisp-unit)
(in-package :lisp-unit)
(setq *print-failures* t)
(load "src/anadiv")

(define-test remove-list
             (assert-equal nil (remove-list '(d e) '(a b c)))
             (assert-equal nil (remove-list '(d c) '(a b c)))
             (assert-equal nil (remove-list '(a d c) '(a b c)))
             (assert-equal nil (remove-list '(a a e) '(a b c)))
             (assert-equal '(d c) (remove-list '(a b) '(a b d c)))
             (assert-equal '(d c) (remove-list '(a a) '(d a a c)))
             )

(define-test extract-digits
             (assert-equal '((0 7) (8 4)) (extract-digits (digits-from-number 48) (digits-from-number 4807)))
             )
(define-test number-pair-from-string
             (assert-equal (cons (digits-from-number 4807) (list 10)) (number-pair-from-string "4807 10"))
             )

(define-test string-from-digits
             (assert-equal "4807" (string-from-digits (digits-from-number 4807)))
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
             ; (assert-equal nil (divisible-by 7 (digits-from-number 6)))
             ; (assert-equal t (divisible-by 7 (digits-from-number (* 7 7 7 7 7 7 7 7 7 7 7))))
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
             (assert-equal (list 0 4 7 8) (max-anagram (digits-from-number 4807)))
             )

(define-test next-anagram
             (assert-equal nil (next-anagram (digits-from-number 4)))
             (assert-equal 14 (value (next-anagram (digits-from-number 41))))
             (assert-equal 27 (value (next-anagram (digits-from-number 72))))
             (assert-equal nil (next-anagram (digits-from-number 14)))
             (assert-equal 4780 (value (next-anagram (digits-from-number 4807))))
             (assert-equal 4708 (value (next-anagram (digits-from-number 4780))))
             (assert-equal 4087 (value (next-anagram (digits-from-number 4708))))
             (assert-equal 4078 (value (next-anagram (digits-from-number 4087))))
             (assert-equal 0874 (value (next-anagram (digits-from-number 4078))))
             (assert-equal 0847 (value (next-anagram (digits-from-number 0874))))
             (assert-equal 0784 (value (next-anagram (digits-from-number 0847))))
             (assert-equal 0748 (value (next-anagram (digits-from-number 0784))))
             (assert-equal 0487 (value (next-anagram (digits-from-number 0748))))
             (assert-equal 0478 (value (next-anagram (digits-from-number 0487))))
             (assert-equal nil (next-anagram (digits-from-number 0478)))
             (assert-equal nil (next-anagram (digits-from-number 1122)))
             (assert-equal 1122 (value (next-anagram (digits-from-number 1212))))
             (assert-equal 12122 (value (next-anagram (digits-from-number 12212))))
             (assert-equal 11221 (value (next-anagram (digits-from-number 12112))))
             (assert-equal 11212 (value (next-anagram (digits-from-number 11221))))
             (assert-equal 11122 (value (next-anagram (digits-from-number 11212))))
             )

(define-test all-anagrams
             (assert-equal '(652 625 562 526 265 256)
                           (mapcar #'value (all-anagrams (digits-from-number 256))))
             )

(define-test max-anagram-divisible-by-5
             (assert-equal nil (max-anagram-divisible-by 5 (digits-from-number 21)))
             )
(define-test max-anagram-divisible-by-7
             (assert-equal 3220 (value (max-anagram-divisible-by 7 (digits-from-number 2023))))
             (assert-equal 8624 (value (max-anagram-divisible-by 7 (digits-from-number 4826))))
             (assert-equal 99888614000 (value (max-anagram-divisible-by 7 (digits-from-number 96889010408))))
             )
(define-test max-anagram-divisible-by-2
             (assert-equal nil (max-anagram-divisible-by-2 nil))
             (assert-equal nil (max-anagram-divisible-by-2 (digits-from-number 1735)))
             (assert-equal 4 (value (max-anagram-divisible-by-2 (digits-from-number 4))))
             (assert-equal 82 (value (max-anagram-divisible-by-2 (digits-from-number 28))))
             (assert-equal 312 (value (max-anagram-divisible-by-2 (digits-from-number 123))))
             (assert-equal 970 (value (max-anagram-divisible-by-2 (digits-from-number 709))))
             (assert-equal 9720 (value (max-anagram-divisible-by-2 (digits-from-number 7092))))
             (assert-equal 975318 (value (max-anagram-divisible-by-2 (digits-from-number 987531))))
             (assert-equal 97533118 (value (max-anagram-divisible-by-2 (digits-from-number 98137531))))
             (assert-equal 
               99999999999999999999777777777777777777775555555555555555555533333333333333333333111111111111111111118
                           (value
                             (max-anagram-divisible-by-2
                               (cons 8 (loop for d from 1 to 100 collect (rem (1+ (* d 2)) 10))))))

             )

(define-test max-anagram-divisible-by-3
             (assert-equal nil (max-anagram-divisible-by-3 (digits-from-number 4)))
             (assert-equal 99540 (value (max-anagram-divisible-by-3 (digits-from-number (* 3 3 3 3 3 3 3 3 3 3)))))
             (assert-equal nil (max-anagram-divisible-by-3 (digits-from-number (1+ (* 3 3 3 3 3 3 3 3 3 3)))))
             )

(define-test max-anagram-divisible-by-4
             (assert-equal nil (max-anagram-divisible-by-4 (digits-from-number 7)))
             (assert-equal 4 (value (max-anagram-divisible-by-4 (digits-from-number 4))))
             ;(assert-equal 8704 (value (max-anagram-divisible-by 4 (digits-from-number 4807))))
             ;(assert-equal nil (max-anagram-divisible-by 4 (loop for d from 1 to 1000 collect (rem (1+ (* d 2)) 10))))
             )
(define-test max-anagram-divisible-by-5
             (assert-equal nil (max-anagram-divisible-by 5 (digits-from-number 4826882468862)))
             )
(define-test max-anagram-divisible-by-6
             (assert-equal nil (max-anagram-divisible-by 6 (digits-from-number 4827834987386)))
             )
(define-test max-anagram-divisible-by-8
             (assert-equal nil (max-anagram-divisible-by 8 (loop for d from 1 to 1000 collect (rem (1+ (* d 2)) 10))))
             )
(define-test max-anagram-divisible-by-9
             (assert-equal nil (max-anagram-divisible-by 9 (digits-from-number 4827834987386)))
             )
(define-test max-anagram-divisible-by-10
             (assert-equal nil (max-anagram-divisible-by 10 (digits-from-number 4827834987386)))
             )
(run-tests :all)
(sb-ext:quit)
