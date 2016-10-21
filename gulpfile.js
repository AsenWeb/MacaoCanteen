//var gulp = require("gulp"),
//    less = require("gulp-less");
//    
//    gulp.task("test", function () {
//    gulp.src("From.less")
//        .pipe(less())
//        .pipe(gulp.dest("css"));


//    });

var gulp = require('gulp'),
    less = require('gulp-less');

gulp.task('base', function () {
    gulp.src('control/*').pipe(less()).pipe(gulp.dest('css'));

});

gulp.task('testLess', function () {
    gulp.src(['page/Index.less'])
        .pipe(less())
        .pipe(gulp.dest('css'));
});