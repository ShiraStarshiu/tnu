var fs = require('fs');

var text = fs.readFileSync('a.txt').toString().split('\n'),
    key = text[0].toUpperCase(),
    params = getMatrix(key),
    matrix = params.matrix,
    used = params.used;

    console.log(matrix);
    console.log(getCipher(text, used));

function decodeRules(first, second) {
    var firstPosition = used[first],
        secondPosition = used[second],
        firstX = firstPosition.x,
        firstY = firstPosition.y,
        secondX = secondPosition.x,
        secondY = secondPosition.y;

    if (firstX == secondX) {
        return matrix[firstX][firstY == 0 ? 4 : firstY - 1] +
               matrix[secondX][secondY == 0 ? 4 : secondY - 1];
    } else if (firstY == secondY) {
        return matrix[firstX == 0 ? 5 : firstX - 1][firstY] +
               matrix[secondX == 0 ? 5 : secondX - 1][secondY];
    }else {
        return matrix[firstX][secondY] +
               matrix[secondX][firstY];
    }

}


function cipherRules(first, second) {
    var firstPosition = used[first],
        secondPosition = used[second],
        firstX = firstPosition.x,
        firstY = firstPosition.y,
        secondX = secondPosition.x,
        secondY = secondPosition.y;

    if (firstX == secondX) {
        return matrix[firstX][firstY == 4 ? 0 : firstY + 1] +
               matrix[secondX][secondY == 4 ? 0 : secondY + 1];
    } else if (firstY == secondY) {
        return matrix[firstX == 5 ? 0 : firstX + 1][firstY] +
               matrix[secondX == 5 ? 0 : secondX + 1][secondY];
    }else {
        return matrix[firstX][secondY] +
               matrix[secondX][firstY];
    }
}

function getCipher(text) {
    var cipher = '';

    text.forEach(function(str, index) {
        if(!index) return;

        var length = str.length;

        str = str.toUpperCase();

        for (var i = 0; i < length; i += 2) {
            var first = str[i],
                second = str[i+1] || '.';

            first == second && (--i, first = '.');
            cipher += decodeRules(first, second);
        }
        cipher += '\n'
    });

    return cipher;
}

function getMatrix(key) {
    var matrix = [[], [], [], [], [], []],
        usedChar = {},
        mIndex = 0,
        push = function(letter) {
            var x = div(mIndex, 5),
                y = mod(mIndex, 5);

            if (usedChar[letter]) return;

            usedChar[letter] = { 'x': x, 'y': y };
            matrix[x][y] = letter;
            mIndex++;
        }

    for (var i in key) { push(key[i]) }
    for (var i = 65; i <= 90; i++) { push(String.fromCharCode(i)) }
    [ ' ', '.', ',', '-'].forEach(function(key){ push(key) });

    return { matrix: matrix,  used: usedChar };
}

function div(x, y){
    return x/y>>0
}

function mod(x, y){
    return x%y>>0
}
