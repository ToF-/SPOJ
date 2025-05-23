(defparameter *size* 101)

(defun parse-operation (line)
  (let ((key (subseq line 4))
        (ope (subseq line 0 3)))
    (list (string-equal ope "ADD") key)))

(defun make-h-table ()
  (make-array *size* :initial-element nil))

(defun set-key-pos (key pos h-table)
      (setf (aref h-table pos) key))

(defun accum (index chars)
  (if chars
    (+ (* index (char-code (car chars)))
       (accum (1+ index) (cdr chars)))
    0))

(defun hash (key)
  (mod (* 19 (accum 1 (coerce key 'list))) *size*))

(defun next-pos (initial-pos index h-table)
  (let ((pos (mod (+ initial-pos (* index index) (* 23 index)) *size*)))
    (cond
      ((= 20 index) nil)
      ((not (aref h-table pos)) pos)
      (t (next-pos initial-pos (1+ index) h-table)))))

(defun find-pos (initial-pos index key h-table)
  (let ((pos (mod (+ initial-pos (* index index) (* 23 index)) *size*)))
    (cond
      ((string-equal key (aref h-table pos)) pos)
      ((= 20 index) nil)
      (t (find-pos initial-pos (1+ index) key h-table)))))

(defun find-key (key h-table)
  (find-pos (hash key) 0 key h-table))

(defun add-key (key h-table)
  (let ((pos (next-pos (hash key) 0 h-table)))
    (cond
      ((find-key key h-table) nil)
      (pos (set-key-pos key pos h-table))
      (t nil))))

(defun delete-key (key h-table)
  (let ((pos (find-key key h-table)))
    (cond
      (pos (set-key-pos nil pos h-table))
      (t nil))))

(defun nb-keys-index (index h-table)
  (cond
    ((= *size* index) 0)
    (t (+ (if (aref h-table index) 1 0)
          (nb-keys-index (1+ index) h-table)))))

(defun nb-keys (h-table)
  (nb-keys-index 0 h-table))

(defun print-h-table (h-table)
  (let ((nb (nb-keys h-table)))
    (progn
      (format t "~D~%" nb)
      (loop for pos from 0 to (1- *size*) do
            (let ((key (aref h-table pos)))
              (if key
                (format t "~D:~A~%" pos key)
                nil))))))

(defun process-case ()
  (let ((nb-lines (read))
        (h-table (make-h-table)))
    (progn
      (loop for i from 1 to nb-lines do
            (let* ((line (read-line))
                   (oper (parse-operation line))
                   (is-add (car oper))
                   (key (cadr oper)))
              (if is-add
                (add-key key h-table)
                (delete-key key h-table))
              ))
      (print-h-table h-table))))

(defun process ()
  (let ((nb-cases (read)))
    (loop for i from 1 to nb-cases do
          (process-case))))

(process)
