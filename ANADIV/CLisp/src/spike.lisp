; read pairs of numbers on each line and print them

(defun read-pair ()
  (handler-case
    (let* ((line (concatenate 'string "(" (read-line) ")"))
             (input (make-string-input-stream line))
             (pair (read input)))
      pair)
    (end-of-file () nil)))

(defun process ()
  (let ((pair (read-pair)))
    (if pair
      (progn
        (format t "~A ~A ~%" pair (type-of pair))
        (process))
      )))

(process)
