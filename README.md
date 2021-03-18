# Restaurant_Puzzle

This repository contains the solution to Jurgensville Restaurant Puzzle in C#.

Below is the problem statement:

We are given a CSV file containing the prices of every item on every menu of every restaurant in the town of Jurgensville. In addition, the restaurants of Jurgensville also offer Value Meals, which are groups of several items, at a discounted price. Below is the file's format

for lines that define a price for a single item:

restaurant ID, price, item label

for lines that define the price for a Value Meal (there can be any number of items in a value meal)

restaurant ID, price, item 1 label, item 2 label, ...

All restaurant IDs are integers, all item labels are lower case letters and underscores, and the price is a decimal number.

We need a program that accepts...

the town's price file
a list of item labels that someone wants to eat for dinner
and outputs
the restaurant they should go to
the total price it will cost them
It is okay to purchase extra items if the total cost is minimized.
Here are some sample data sets, program inputs, and the expected result:

Data File data.csv

1, 4.00, burger 1, 8.00, tofu_log
2, 5.00, burger 2, 6.50, tofu_log

Program Input

> program data.csv burger tofu_log

Expected Output

=> 2, 11.5

Data File data.csv

3, 4.00, chef_salad
3, 8.00, steak_salad_sandwich
4, 5.00, steak_salad_sandwich
4, 2.50, wine_spritzer

Program Input

> program data.csv chef_salad, wine_spritzer

Expected Output

=> null

Data File data.csv

5, 4.00, extreme_fajita
5, 8.00, fancy_european_water
6, 5.00, fancy_european_water
6, 6.00, extreme_fajita, jalapeno_poppers, extra_salsa

Program Input

 > program data.csv fancy_european_water extreme_fajita
Expected Output

=> 6, 11.0
