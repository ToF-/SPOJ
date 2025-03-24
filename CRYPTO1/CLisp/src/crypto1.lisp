; -------- crypto1.lisp ---------

(defconstant *day-names* '("Mon" "Tue" "Wed" "Thu" "Fri" "Sat" "Sun"))
(defconstant *month-names* '("Foo" "Jan" "Feb" "Mar" "Apr" "May" "Jun" "Jul" "Aug" "Sep" "Oct" "Nov" "Dec"))

(defun unix-timestamp (timestamp)
  (- timestamp 2198988800))

(defun universal-timestamp (timestamp)
  (+ timestamp 2198988800))

(defun print-time (timestamp)
  (multiple-value-bind
    (second minute hour day month year day-of-week dst-p tz)
    (decode-universal-time timestamp)
    (format nil "~A ~A ~D ~2,'0D:~2,'0D:~2,'0D ~D"  
           (nth day-of-week *day-names*) 
           (nth month *month-names*)
           day
           hour
           minute
           second
           year)))

(defparameter *p* 4000000007)
(defparameter *k* 1000000001)

(defun target-time (a)
    (print-time (unix-timestamp (power a (1+ *k*) *p*))))

(defun unix-timestamp (timestamp)
  (- timestamp 2198988800))

(defun power (a p m)
  (cond
    ((= p 0) 1)
    ((= p 1) (mod a m))
    ((evenp p) (mod (power (mod (* a a) m) (floor p 2) m) m))
    ((oddp p) (mod (* a (power (mod (* a a) m) (floor (1- p) 2) m)) m))
    ))

(defun process ()
  (progn
    (format t "~A~%" (target-time (read)))))
