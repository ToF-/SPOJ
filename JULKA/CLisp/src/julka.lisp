(defun compute-apples (apple-total klaudia-surplus)
  (let* ((klaudia-apples (floor (+ apple-total klaudia-surplus) 2))
         (natalia-apples (- apple-total klaudia-apples)))
    (list klaudia-apples natalia-apples)))

(defun process-case ()
  (let* ((apple-total (read))
         (klaudia-surplus (read))
         (result (compute-apples apple-total klaudia-surplus)))
      (format t "~D~%~D~%" (car result) (cadr result))))

(defun process ()
  (loop for i from 1 to 10 do
        (process-case)))
