# BITMAP - Bitmap

There is given a rectangular bitmap of size _n_ x _m_. Each pixel of the bitmap is either white or black, but at least one is white. The pixel in _i_-th line and _j_-th column is called the pixel _(i,j)_. The distance between two pixels _p1=(i1,j1)_ and _p2=(i2,j2)_ is defined as:

    d(p1,p2)=|i1-i2|+|j1-j2|.

## Task
Write a program which:

- reads the description of the bitmap from the standard input,
- for each pixel, computes the distance to the nearest white pixel,
- writes the results to the standard output.

## Input
The number of test cases _t_ is in the first line of input, then _t_ test cases follow separated by an empty line. In the first line of each test case there is a pair of integer numbers _n_, _m_ separated by a single space, _1<=n <=182_, _1<=m<=182_. In each of the following _n_ lines of the test case exactly one zero-one word of length _m_, the description of one line of the bitmap, is written. On the _j_-th position in the line _(i+1)_, _1 <= i <= n_, _1 <= j <= m_, is '1' if, and only if the pixel _(i,j)_ is white.

## Output
In the _i_-th line for each test case, _1<=i<=n_, there should be written m integers _f(i,1),...,f(i,m)_ separated by single spaces, where _f(i,j)_ is the distance from the pixel _(i,j)_ to the nearest white pixel.

## Example
### Sample input:

    1
    3 4
    0001
    0011
    0110

### Sample output:

    3 2 1 0
    2 1 0 0
    1 0 0 1
