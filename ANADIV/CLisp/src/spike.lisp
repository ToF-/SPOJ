
(ql:quickload "str")

; read numbers on each line until eof and print numbers with index
; returns a list of numbers


(defun spike ()
  (let* ((data (read-line *standard-input*))
         (words (str:words data)))
    (if data
      (format t "~A ~A ~%" words (type-of words))
      (format t "EOF ~%"))))

(spike)
(spike)
(spike)

