(defun parse-operation (line)
  (let ((key (subseq line 4))
        (ope (subseq line 0 3)))
    (list (string-equal ope "ADD") key)))
