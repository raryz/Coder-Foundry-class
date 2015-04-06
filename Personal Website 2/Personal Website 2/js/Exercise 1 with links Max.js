function maxOfFive(value1,value2,value3,value4,value5,resultElementName) {
    var resultElement = document.getElementById(resultElementName);
    var holdNumber = [] ;
    var maxnumber;
    holdNumber[0] = Number(value1);
    holdNumber[1] = Number(value2);
    holdNumber[2] = Number(value3);
    holdNumber[3] = Number(value4);
    holdNumber[4] = Number(value5);
    maxnumber = holdNumber[0];
    
        for (var i = 0; i < 4; i++) {
            if (maxnumber < holdNumber[i + 1]) {
                maxnumber = holdNumber[i+1];
            }
            
        }
        
        return resultElement.innerHTML = 'The maximum number is ' + maxnumber.toString();
                
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
    return resultElement.innerHTML = 'The sum of the numbers in the array is ' + sumNumber.toString();

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
    return resultElement.innerHTML = 'The product of the numbers in the array is ' + multNumber.toString();

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