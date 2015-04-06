function maxOfFive(value1, value2, value3, value4, value5, resultElementName) {
    var resultElement = document.getElementById(resultElementName);
    var holdNumber = [];
    var maxnumber;
    holdNumber[0] = Number(value1);
    holdNumber[1] = Number(value2);
    holdNumber[2] = Number(value3);
    holdNumber[3] = Number(value4);
    holdNumber[4] = Number(value5);
    maxnumber = holdNumber[0];

    for (var i = 0; i < 4; i++) {
        if (maxnumber < holdNumber[i + 1]) {
            maxnumber = holdNumber[i + 1];
        }

    }

    return resultElement.textContent = 'The maximum number is ' + maxnumber.toString();

}

function sum(value1, value2, value3, value4, value5, resultElementName) {
    var resultElement = document.getElementById(resultElementName);
    var holdNumber = [];
    var sumNumber = 0 ;
    holdNumber[0] = Number(value1);
    holdNumber[1] = Number(value2);
    holdNumber[2] = Number(value3);
    holdNumber[3] = Number(value4);
    holdNumber[4] = Number(value5);
    for (var i = 0; i < 5; i++) {
        sumNumber += holdNumber[i];
    }
    return resultElement.textContent = 'The sum is ' + sumNumber.toString();

}

function multiply(value1, value2, value3, value4, value5, resultElementName) {
    var resultElement = document.getElementById(resultElementName);
    var holdNumber = [];
    var multNumber = 1;
    holdNumber[0] = Number(value1);
    holdNumber[1] = Number(value2);
    holdNumber[2] = Number(value3);
    holdNumber[3] = Number(value4);
    holdNumber[4] = Number(value5);
    for (var i = 0; i < 5; i++) {
        multNumber *= holdNumber[i];
    }
    return resultElement.textContent = 'The product is ' + multNumber.toString();
}

function palindrome(value1, resultElementName) {
    var resultElement = document.getElementById(resultElementName);
    var sourceString = value1;
    var lowSource = sourceString.toLowerCase();
    var palTrue = true;
    var lastIndex = lowSource.length - 1 ;
    var startIndex = 0;
    while((startIndex < lastIndex) & (palTrue))
    if (lowSource[startIndex] == lowSource[lastIndex] ) {
        startIndex++;
        lastIndex--;
    } else {
      palTrue = false;
    }
    if (palTrue == true) {
        return resultElement.innerHTML = sourceString + ' is a palindrome.';
    }
    else {
        return resultElement.innerHTML = sourceString + ' is a not palindrome.';
    }
}

function factorial(value1, resultElementName) {
    var resultElement = document.getElementById(resultElementName);
    var num1 = Number(value1);
    var answer = 1;
    for (var i = 1; i <= num1 ; i++) {
        answer *= i;
    }
    return resultElement.innerHTML = 'The result is ' + answer.toString();
}

function iterate(resultElementName) {
    var resultElement = document.getElementById(resultElementName);
    var holdresult = [];
    holdresult[0] = 'Results = ';

    for (var i = 1; i <= 100; i++) {
        if ((i % 3 == 0) & (i % 5 == 0)) {
            holdresult[i] = 'FizzBuzz';
        } else if (i % 3 == 0) {
            holdresult[i] = 'Fizz';
        } else if (i % 5 == 0) {
            holdresult[i] = 'Buzz';
        } else {
            holdresult[i] = i.toString();
        }

    }
    return resultElement.innerHTML = holdresult;




}

function armstrongCalc(resultElementName) {
    var resultElement = document.getElementById(resultElementName);
    var result = " ";


    for (var i = 100; i < 1000 ; i++) {
        var num = i;
        var sum = 0;
        var count = 1;
        while (count <= 3) {
            sum += Math.pow((num % 10), 3);
            num = Math.floor(num / 10);
            count += 1;
        }
        if (sum == i) {
            result = result + i.toString() + ', ';
        }
    }

    var lastchar = result.length;

    result = result.substring(0, (lastchar - 2));

    return resultElement.textContent = 'The Armstrong numbers are : ' + result;

}   // end function armstrongCalc

function checkPerfect(pNum1, resultElementName) {
    var resultElement = document.getElementById(resultElementName);
    var checkNum = Number(pNum1);
    var sum = 0;

    for (var i = 1; i < checkNum; i++) {
        if ((checkNum % i) == 0) {
            sum += i;
        }
    }
    if (sum == checkNum) {
        return resultElement.textContent = checkNum.toString() + ' is a Perfect number.'
    }
    else {
        return resultElement.textContent = checkNum.toString() + ' is not a Perfect number.'
    }
}

function dispPerfect(resultElementName) {
    var resultElement = document.getElementById(resultElementName);
    var checkNum = 1;
    var sum = 0;
    var holdResult = [];
    var r = 1;
    holdResult[0] = 'The Perfect numbers are ';

    while (checkNum <= 10000) {

        for (var i = 1; i < checkNum; i++) {
            if ((checkNum % i) == 0) {
                sum += i;
            }
        }
        if (sum == checkNum) {
            holdResult[r] = i.toString();
            console.log(holdResult[r]);
            r += 1;
        }
        sum = 0;
        checkNum += 1;
    }
    return resultElement.textContent = holdResult;
}

function findLongestWord(evt) {      // evt is the 'change' event object in this context
    var output = [];
    var resultElement = document.getElementById('ResultLW');
    var f = evt.target.files[0];    // event object evt has property 'target' and the target in this case
    // are the files
    if (f) {
        var reader = new FileReader();         // filereader object

        reader.onload = function (e) {   // defining the function it is executed once
            alert(reader.result);       // .readAsText() calls it after that has finish executing
            // once the FileReader has finished reading via reader.readAsText(f)

            var fullText = reader.result;
            var fullLength = fullText.length;
            var countLength = 0;
            var startPos = 0;
            var stopPos = 0;
            var max = 0, maxstart = 0, maxstop = 0, i = 0, j = 0;

            while (i < fullLength) {
                j = startPos;
                while ((fullText.charAt(j) != " ") && (j < fullLength)) {
                    countLength += 1;
                    stopPos += 1;
                    j += 1;
                }
                if (stopPos < j) {
                    stopPos += 1;
                }
                if (countLength > max) {
                    max = countLength;
                    maxstart = startPos;
                    maxstop = stopPos - 1;
                }
                startPos = stopPos + 1;
                countLength = 0;
                i = startPos;
            }                             // end while i < fullLength

            for (var k = maxstart; k <= maxstop; k++) {
                output += fullText.charAt(k);
            }
            return resultElement.textContent = 'The longest word is ' + output;
        }
        // reads the file as text and then calls the function the onload is linked to 
        reader.readAsText(f);
    }                                  // end if(f)
    else {
        alert("Failed to load file");
    }

}

document.getElementById('files').addEventListener('change', findLongestWord, false);


function happy_test(resultElementName) {
    var resultElement = document.getElementById(resultElementName);
    var isHappy = false;
    var number = 1;
    var found5 = false;
    var happyCount = 0;

    var holdHappy = [];


    var digitcount = 0;

    while (!found5) {
        isHappy = checkHappy(number);
        if (isHappy) {
            holdHappy[happyCount] = number.toString();
            happyCount += 1;
        }
        if (happyCount == 5) {
            found5 = true;
        }
        number++;
        isHappy = false;
    }                                            // end while !found5

    function checkHappy(number) {
        var i = 0;
        var j = 0;
        var sum = 0;

        var holdPrevFound = [];

        while (!isHappy) {

            var output = [];

            while (number) {

                digitcount = output.unshift(number % 10);
                number = Math.floor(number / 10);
            }
            while (i < digitcount) {
                sum += Math.pow(output[i], 2);
                i += 1;
            }
            if (sum == 1) {
                isHappy = true;
                return isHappy;
            }

            else {

                for (var k = 0; k < holdPrevFound.length; k++) {
                    if (sum == holdPrevFound[k]) {
                        isHappy = false;
                        return isHappy;
                    }
                }
                number = sum;
                holdPrevFound[holdPrevFound.length] = sum;
                sum = 0;
                i = 0;
            }
        }   // end while  ( !isHappy )
    }    // end checkHappy

    return resultElement.textContent = 'The first 5 Happy numbers are : ' + holdHappy.toString();

}   // end function happy_test
