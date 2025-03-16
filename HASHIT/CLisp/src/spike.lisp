
; read a number N, then read N lines and print them

(defun spike ()
  (let ((nb-cases (read)))
    (loop for i from 1 to nb-cases do
          (let ((nb-lines (read)))
            (loop for i from 1 to nb-lines do
                  (let ((line (read-line)))
                    (format t "~A~%" line)))))))
(spike)

