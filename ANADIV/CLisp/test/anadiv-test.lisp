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
             (assert-equal '(d a c) (remove-list '(a a) '(d a a a c)))
             )

(define-test list-<
             (assert-equal t (list-< '(0 1 2) '(0 2 1)))
             (assert-equal nil (list-< '(3 1 2) '(0 2 1)))
             (assert-equal nil (list-< '(0 1 2) '(0 1 2)))
             (assert-equal t (list-< '(0 1) '(0 1 2)))
             (assert-equal nil (list-< '(0 1 2) '(0 1)))
             )

(define-test list-sort
             (assert-equal '((0 1) (0 2) (1 0) (1 2)) (list-sort '((0 2) (1 2) (0 1) (1 0))))
             )

(define-test digits-from-number
             (assert-equal '(3) (digits-from-number 3))
             (assert-equal '(7) (digits-from-number 7))
             (assert-equal (reverse '(4 2)) (digits-from-number 42))
             (assert-equal (reverse '(1 7)) (digits-from-number 17))
             (assert-equal (reverse '(4 8 0 7)) (digits-from-number 4807))
             )

(define-test number-from-digits
             (assert-equal 4807 (number-from-digits (list 7 0 8 4)))
             (assert-equal 42 (number-from-digits (list 2 4 0 0 0)))
             )

(define-test number-pair-from-string
             (assert-equal (cons (digits-from-number 4807) '(10)) (number-pair-from-string "4807 10"))
             (assert-equal (cons (digits-from-number 2317) '(7)) (number-pair-from-string "2317 7"))
             )

(define-test string-from-digits
             (assert-equal "4807" (string-from-digits (digits-from-number 4807)))
             )

(define-test max-anagram
             (assert-equal '(0 4 7 8) (max-anagram (digits-from-number 4807)))
             )

(define-test multiples
             (assert-equal '((0) (2) (4) (6) (8)) *multiples-of-2*)
             (assert-equal
               '((0 0) (4 0) (8 0) (2 1) (6 1) (0 2)
                       (4 2) (8 2) (2 3) (6 3) (0 4)
                       (4 4) (8 4) (2 5) (6 5) (0 6)
                       (4 6) (8 6) (2 7) (6 7) (0 8)
                       (4 8) (8 8) (2 9) (6 9)) 
               *multiples-of-4*)
             (assert-equal
               '((0 0 0) (8 0 0) (6 1 0) (4 2 0) (2 3 0) (0 4 0) (8 4 0) (6 5 0)
                         (4 6 0) (2 7 0) (0 8 0) (8 8 0) (6 9 0) (4 0 1) (2 1 1) (0 2 1)
                         (8 2 1) (6 3 1) (4 4 1) (2 5 1) (0 6 1) (8 6 1) (6 7 1) (4 8 1)
                         (2 9 1) (0 0 2) (8 0 2) (6 1 2) (4 2 2) (2 3 2) (0 4 2) (8 4 2)
                         (6 5 2) (4 6 2) (2 7 2) (0 8 2) (8 8 2) (6 9 2) (4 0 3) (2 1 3)
                         (0 2 3) (8 2 3) (6 3 3) (4 4 3) (2 5 3) (0 6 3) (8 6 3) (6 7 3)
                         (4 8 3) (2 9 3) (0 0 4) (8 0 4) (6 1 4) (4 2 4) (2 3 4) (0 4 4)
                         (8 4 4) (6 5 4) (4 6 4) (2 7 4) (0 8 4) (8 8 4) (6 9 4) (4 0 5)
                         (2 1 5) (0 2 5) (8 2 5) (6 3 5) (4 4 5) (2 5 5) (0 6 5) (8 6 5)
                         (6 7 5) (4 8 5) (2 9 5) (0 0 6) (8 0 6) (6 1 6) (4 2 6) (2 3 6)
                         (0 4 6) (8 4 6) (6 5 6) (4 6 6) (2 7 6) (0 8 6) (8 8 6) (6 9 6)
                         (4 0 7) (2 1 7) (0 2 7) (8 2 7) (6 3 7) (4 4 7) (2 5 7) (0 6 7)
                         (8 6 7) (6 7 7) (4 8 7) (2 9 7) (0 0 8) (8 0 8) (6 1 8) (4 2 8)
                         (2 3 8) (0 4 8) (8 4 8) (6 5 8) (4 6 8) (2 7 8) (0 8 8) (8 8 8)
                         (6 9 8) (4 0 9) (2 1 9) (0 2 9) (8 2 9) (6 3 9) (4 4 9) (2 5 9)
                         (0 6 9) (8 6 9) (6 7 9) (4 8 9) (2 9 9))
               *multiples-of-8*)
             )

(define-test find-multiple-digits
             (assert-equal nil (find-multiple-digits '(4 8 0 7) '(2)))
             (assert-equal '((0) (4 8 7)) (find-multiple-digits '(4 8 0 7) '(0)))
             (assert-equal '((0 4) (8 7)) (find-multiple-digits '(4 8 0 7) '(0 4)))
             (assert-equal nil (find-multiple-digits '(4 8 0 7) '(8 4 2)))
             (assert-equal '((2 3) (7 6 8)) (find-multiple-digits '(3 2 7 6 8) '(2 3)))
             )

(define-test find-largest-multiple-anagram
             (assert-equal nil (find-largest-multiple-anagram '(5 3 1 7) *multiples-of-4*))
             (assert-equal '(0 4 7 8) (find-largest-multiple-anagram '(4 8 0 7) *multiples-of-4*))
             (assert-equal '(2 3 6 7 8) (find-largest-multiple-anagram '(3 2 7 6 8) *multiples-of-8*))
             (assert-equal '(2 1 3 7) (find-largest-multiple-anagram '(2 3 1 7) *multiples-of-4*))
             (assert-equal '(8 1 1 1 1 3 3 3 3 5 5 5 5 7 7 7 7 9 9 9)
                           (find-largest-multiple-anagram (digits-from-number 99987777555533331111) *multiples-of-2*))
             (assert-equal '(2 1 1 1 1 3 3 3 3 5 5 5 5 7 7 7 7 8 9 9 9)
                           (find-largest-multiple-anagram (digits-from-number 999877277555533331111) *multiples-of-4*))
            
             )
; ***************************

(define-test subtract
             (defun assert-subtract (result minuend subtrahend)
               (assert-equal result
                             (number-from-digits (subtract
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
                             (number-from-digits (add
                                      (digits-from-number operand)
                                      (digits-from-number addend)))))

             (assert-add 7 3 4)
             (assert-add 9 5 4)
             (assert-add 10 5 5)
             (assert-add 243 210 33)
             )

(define-test sum-digits
             (defun assert-sum-digits (result operand)
               (assert-equal result (number-from-digits (sum-digits (digits-from-number operand)))))

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

(define-test next-anagram
             (assert-equal nil (next-anagram (digits-from-number 4)))
             (assert-equal 14 (number-from-digits (next-anagram (digits-from-number 41))))
             (assert-equal 27 (number-from-digits (next-anagram (digits-from-number 72))))
             (assert-equal nil (next-anagram (digits-from-number 14)))
             (assert-equal 4780 (number-from-digits (next-anagram (digits-from-number 4807))))
             (assert-equal 4708 (number-from-digits (next-anagram (digits-from-number 4780))))
             (assert-equal 4087 (number-from-digits (next-anagram (digits-from-number 4708))))
             (assert-equal 4078 (number-from-digits (next-anagram (digits-from-number 4087))))
             (assert-equal 0874 (number-from-digits (next-anagram (digits-from-number 4078))))
             (assert-equal 0847 (number-from-digits (next-anagram (digits-from-number 0874))))
             (assert-equal 0784 (number-from-digits (next-anagram (digits-from-number 0847))))
             (assert-equal 0748 (number-from-digits (next-anagram (digits-from-number 0784))))
             (assert-equal 0487 (number-from-digits (next-anagram (digits-from-number 0748))))
             (assert-equal 0478 (number-from-digits (next-anagram (digits-from-number 0487))))
             (assert-equal nil (next-anagram (digits-from-number 0478)))
             (assert-equal nil (next-anagram (digits-from-number 1122)))
             (assert-equal 1122 (number-from-digits (next-anagram (digits-from-number 1212))))
             (assert-equal 12122 (number-from-digits (next-anagram (digits-from-number 12212))))
             (assert-equal 11221 (number-from-digits (next-anagram (digits-from-number 12112))))
             (assert-equal 11212 (number-from-digits (next-anagram (digits-from-number 11221))))
             (assert-equal 11122 (number-from-digits (next-anagram (digits-from-number 11212))))
             )

(define-test all-anagrams
             (assert-equal '(652 625 562 526 265 256)
                           (mapcar #'number-from-digits (all-anagrams (digits-from-number 256))))
             )

(define-test max-anagram-divisible-by-5
             (assert-equal nil (max-anagram-divisible-by 5 (digits-from-number 21)))
             )
(define-test max-anagram-divisible-by-7
             (assert-equal 3220 (number-from-digits (max-anagram-divisible-by 7 (digits-from-number 2023))))
             (assert-equal 8624 (number-from-digits (max-anagram-divisible-by 7 (digits-from-number 4826))))
             (assert-equal 99888614000 (number-from-digits (max-anagram-divisible-by 7 (digits-from-number 96889010408))))
             )
(define-test max-anagram-divisible-by-2
             (assert-equal nil (max-anagram-divisible-by-2 nil))
             (assert-equal nil (max-anagram-divisible-by-2 (digits-from-number 1735)))
             (assert-equal 4 (number-from-digits (max-anagram-divisible-by-2 (digits-from-number 4))))
             (assert-equal 82 (number-from-digits (max-anagram-divisible-by-2 (digits-from-number 28))))
             (assert-equal 312 (number-from-digits (max-anagram-divisible-by-2 (digits-from-number 123))))
             (assert-equal 970 (number-from-digits (max-anagram-divisible-by-2 (digits-from-number 709))))
             (assert-equal 9720 (number-from-digits (max-anagram-divisible-by-2 (digits-from-number 7092))))
             (assert-equal 975318 (number-from-digits (max-anagram-divisible-by-2 (digits-from-number 987531))))
             (assert-equal 97533118 (number-from-digits (max-anagram-divisible-by-2 (digits-from-number 98137531))))
             (assert-equal 
               99999999999999999999777777777777777777775555555555555555555533333333333333333333111111111111111111118
                           (number-from-digits
                             (max-anagram-divisible-by-2
                               (cons 8 (loop for d from 1 to 100 collect (rem (1+ (* d 2)) 10))))))

             )

(define-test max-anagram-divisible-by-3
             (assert-equal nil (max-anagram-divisible-by-3 (digits-from-number 4)))
             (assert-equal 99540 (number-from-digits (max-anagram-divisible-by-3 (digits-from-number (* 3 3 3 3 3 3 3 3 3 3)))))
             (assert-equal nil (max-anagram-divisible-by-3 (digits-from-number (1+ (* 3 3 3 3 3 3 3 3 3 3)))))
             )

(define-test max-anagram-divisible-by-4
             (assert-equal nil (max-anagram-divisible-by-4 (digits-from-number 7)))
             (assert-equal 4 (number-from-digits (max-anagram-divisible-by-4 (digits-from-number 4))))
             ;(assert-equal 8704 (number-from-digits (max-anagram-divisible-by 4 (digits-from-number 4807))))
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
