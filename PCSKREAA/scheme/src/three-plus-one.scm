
(define (three-plus-one n)
  (cond ((= n 1) 1)
        ((odd? n) (+ 1 (three-plus-one (1+ (* n 3)))))
        (else (+ 1 (three-plus-one (/ n 2.0))))))

(define (max-three-plus-one a b)
  (if (> a b)
    0
    (max (three-plus-one a) (max-three-plus-one (1+ a) b))))


(define (process)
  (let loop ()
    (let ((line (read-line)))
      (if (eof-object? line)
          'done
          (begin
            (let* ((numbers (map string->number ((string-splitter) line)))
                   (a (car numbers))
                   (b (cadr numbers)))
              (display a) (display " ") (display b) (display " ") (display (max-three-plus-one a b)))(newline)
            (loop))))))

(process)


