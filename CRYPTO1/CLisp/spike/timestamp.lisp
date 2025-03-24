(defconstant *day-names* '("Mon" "Tue" "Wed" "Thu" "Fri" "Sat" "Sun"))
(defconstant *month-names* '(nil "Jan" "Feb" "Mar" "Apr" "May" "Jun" "Jul" "Aug" "Sep" "Oct" "Nov" "Dec"))

(defun unix-timestamp (timestamp)
  (- timestamp 2198988800))

(defun universal-timestamp (timestamp)
  (+ timestamp 2198988800))

(defun print-time (timestamp)
  (multiple-value-bind
    (second minute hour day month year day-of-week dst-p tz)
    (decode-universal-time timestamp)
    (format t "~a ~a ~d ~2,'0d:~2,'0d:~2,'0d ~d~%"  
           (nth day-of-week *day-names*) 
           (nth month *month-names*)
           day
           hour
           minute
           second
           year)))

(print-time (universal-timestamp (unix-timestamp (get-universal-time))))
