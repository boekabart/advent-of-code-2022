﻿namespace day14;

public static class Input
{
    public const string ExampleInput = @"
498,4 -> 498,6 -> 496,6
503,4 -> 502,4 -> 502,9 -> 494,9
";

    public const string RawInput = @"
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
534,94 -> 538,94
519,92 -> 523,92
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
499,27 -> 499,28 -> 515,28
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
525,88 -> 529,88
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
533,116 -> 537,116
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
523,70 -> 528,70
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
542,107 -> 542,104 -> 542,107 -> 544,107 -> 544,101 -> 544,107 -> 546,107 -> 546,100 -> 546,107
540,126 -> 540,127 -> 543,127 -> 543,126
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
560,161 -> 565,161
540,94 -> 544,94
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
542,107 -> 542,104 -> 542,107 -> 544,107 -> 544,101 -> 544,107 -> 546,107 -> 546,100 -> 546,107
554,167 -> 559,167
544,170 -> 544,174 -> 536,174 -> 536,182 -> 554,182 -> 554,174 -> 549,174 -> 549,170
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
542,107 -> 542,104 -> 542,107 -> 544,107 -> 544,101 -> 544,107 -> 546,107 -> 546,100 -> 546,107
542,107 -> 542,104 -> 542,107 -> 544,107 -> 544,101 -> 544,107 -> 546,107 -> 546,100 -> 546,107
533,122 -> 533,123 -> 541,123
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
543,164 -> 548,164
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
523,44 -> 523,48 -> 517,48 -> 517,55 -> 530,55 -> 530,48 -> 525,48 -> 525,44
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
525,92 -> 529,92
525,119 -> 537,119 -> 537,118
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
530,70 -> 535,70
516,94 -> 520,94
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
544,170 -> 544,174 -> 536,174 -> 536,182 -> 554,182 -> 554,174 -> 549,174 -> 549,170
525,119 -> 537,119 -> 537,118
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
544,170 -> 544,174 -> 536,174 -> 536,182 -> 554,182 -> 554,174 -> 549,174 -> 549,170
528,90 -> 532,90
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
533,67 -> 538,67
546,161 -> 551,161
537,70 -> 542,70
542,107 -> 542,104 -> 542,107 -> 544,107 -> 544,101 -> 544,107 -> 546,107 -> 546,100 -> 546,107
532,61 -> 537,61
499,27 -> 499,28 -> 515,28
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
534,90 -> 538,90
531,88 -> 535,88
556,158 -> 561,158
525,61 -> 530,61
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
541,130 -> 541,133 -> 537,133 -> 537,138 -> 548,138 -> 548,133 -> 545,133 -> 545,130
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
546,141 -> 546,144 -> 541,144 -> 541,152 -> 554,152 -> 554,144 -> 550,144 -> 550,141
523,44 -> 523,48 -> 517,48 -> 517,55 -> 530,55 -> 530,48 -> 525,48 -> 525,44
533,122 -> 533,123 -> 541,123
546,141 -> 546,144 -> 541,144 -> 541,152 -> 554,152 -> 554,144 -> 550,144 -> 550,141
557,164 -> 562,164
552,155 -> 557,155
531,92 -> 535,92
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
544,170 -> 544,174 -> 536,174 -> 536,182 -> 554,182 -> 554,174 -> 549,174 -> 549,170
547,167 -> 552,167
523,44 -> 523,48 -> 517,48 -> 517,55 -> 530,55 -> 530,48 -> 525,48 -> 525,44
542,107 -> 542,104 -> 542,107 -> 544,107 -> 544,101 -> 544,107 -> 546,107 -> 546,100 -> 546,107
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
561,167 -> 566,167
522,64 -> 527,64
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
519,67 -> 524,67
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
523,44 -> 523,48 -> 517,48 -> 517,55 -> 530,55 -> 530,48 -> 525,48 -> 525,44
550,164 -> 555,164
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
516,70 -> 521,70
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
523,44 -> 523,48 -> 517,48 -> 517,55 -> 530,55 -> 530,48 -> 525,48 -> 525,44
536,64 -> 541,64
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
539,110 -> 543,110
529,64 -> 534,64
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
528,86 -> 532,86
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
540,126 -> 540,127 -> 543,127 -> 543,126
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
545,116 -> 549,116
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
546,141 -> 546,144 -> 541,144 -> 541,152 -> 554,152 -> 554,144 -> 550,144 -> 550,141
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
544,170 -> 544,174 -> 536,174 -> 536,182 -> 554,182 -> 554,174 -> 549,174 -> 549,170
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
541,130 -> 541,133 -> 537,133 -> 537,138 -> 548,138 -> 548,133 -> 545,133 -> 545,130
568,167 -> 573,167
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
546,141 -> 546,144 -> 541,144 -> 541,152 -> 554,152 -> 554,144 -> 550,144 -> 550,141
546,141 -> 546,144 -> 541,144 -> 541,152 -> 554,152 -> 554,144 -> 550,144 -> 550,141
553,161 -> 558,161
541,130 -> 541,133 -> 537,133 -> 537,138 -> 548,138 -> 548,133 -> 545,133 -> 545,130
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
546,141 -> 546,144 -> 541,144 -> 541,152 -> 554,152 -> 554,144 -> 550,144 -> 550,141
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
540,67 -> 545,67
541,130 -> 541,133 -> 537,133 -> 537,138 -> 548,138 -> 548,133 -> 545,133 -> 545,130
546,141 -> 546,144 -> 541,144 -> 541,152 -> 554,152 -> 554,144 -> 550,144 -> 550,141
523,44 -> 523,48 -> 517,48 -> 517,55 -> 530,55 -> 530,48 -> 525,48 -> 525,44
522,90 -> 526,90
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
528,58 -> 533,58
549,158 -> 554,158
542,107 -> 542,104 -> 542,107 -> 544,107 -> 544,101 -> 544,107 -> 546,107 -> 546,100 -> 546,107
541,130 -> 541,133 -> 537,133 -> 537,138 -> 548,138 -> 548,133 -> 545,133 -> 545,130
523,44 -> 523,48 -> 517,48 -> 517,55 -> 530,55 -> 530,48 -> 525,48 -> 525,44
526,67 -> 531,67
540,167 -> 545,167
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
544,170 -> 544,174 -> 536,174 -> 536,182 -> 554,182 -> 554,174 -> 549,174 -> 549,170
544,170 -> 544,174 -> 536,174 -> 536,182 -> 554,182 -> 554,174 -> 549,174 -> 549,170
541,130 -> 541,133 -> 537,133 -> 537,138 -> 548,138 -> 548,133 -> 545,133 -> 545,130
541,130 -> 541,133 -> 537,133 -> 537,138 -> 548,138 -> 548,133 -> 545,133 -> 545,130
540,126 -> 540,127 -> 543,127 -> 543,126
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
542,113 -> 546,113
528,94 -> 532,94
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
522,94 -> 526,94
539,116 -> 543,116
544,70 -> 549,70
542,107 -> 542,104 -> 542,107 -> 544,107 -> 544,101 -> 544,107 -> 546,107 -> 546,100 -> 546,107
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
536,113 -> 540,113
537,92 -> 541,92
507,41 -> 507,39 -> 507,41 -> 509,41 -> 509,35 -> 509,41 -> 511,41 -> 511,34 -> 511,41 -> 513,41 -> 513,40 -> 513,41 -> 515,41 -> 515,38 -> 515,41 -> 517,41 -> 517,31 -> 517,41 -> 519,41 -> 519,33 -> 519,41 -> 521,41 -> 521,32 -> 521,41 -> 523,41 -> 523,39 -> 523,41
513,83 -> 513,76 -> 513,83 -> 515,83 -> 515,73 -> 515,83 -> 517,83 -> 517,80 -> 517,83 -> 519,83 -> 519,82 -> 519,83 -> 521,83 -> 521,80 -> 521,83 -> 523,83 -> 523,73 -> 523,83 -> 525,83 -> 525,81 -> 525,83 -> 527,83 -> 527,81 -> 527,83 -> 529,83 -> 529,74 -> 529,83
564,164 -> 569,164
490,23 -> 490,17 -> 490,23 -> 492,23 -> 492,14 -> 492,23 -> 494,23 -> 494,20 -> 494,23 -> 496,23 -> 496,18 -> 496,23 -> 498,23 -> 498,19 -> 498,23 -> 500,23 -> 500,20 -> 500,23 -> 502,23 -> 502,17 -> 502,23 -> 504,23 -> 504,14 -> 504,23 -> 506,23 -> 506,18 -> 506,23 -> 508,23 -> 508,15 -> 508,23
";
}