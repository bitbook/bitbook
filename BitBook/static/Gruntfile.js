// Gruntfile.js
module.exports = function (grunt) {
     grunt.loadNpmTasks('grunt-bower-concat');
grunt.loadNpmTasks('grunt-contrib-uglify');
grunt.loadNpmTasks('grunt-shell');
grunt.initConfig({
bower_concat: {
  all: {
    dest: 'js/bower.js',
    cssDest: 'css/bower.css',
    mainFiles: {
      // 'jQuery': ['src/ajax.js']
    }
  }
},
uglify: {
   bower: {
    options: {
      mangle: true,
      compress: true,
        sourceMap: true,
sourceMapIncludeSources: true,
        sourceMapName: 'js/bower.map'
    },
    files: {
      'js/bower.min.js': 'js/bower.js'
    }
  }
},
shell: {
  // ...
  bowerinstall: {
    command: function(libname){
      return 'bower install ' + libname + ' -S';
    }
  },
  bowerupdate: {
    command: function(libname){
      return 'bower update ' + libname;
    }
  }
}      });

    grunt.registerTask('default', []);
    
    grunt.registerTask('buildbower', [
      'bower_concat',
      'uglify:bower'
    ]);
    grunt.registerTask('bowerinstall', function(library) {
      grunt.task.run('shell:bowerinstall:' + library);
      grunt.task.run('buildbower');
    });
  
    grunt.registerTask('bowerupdate', function(library) {
      grunt.task.run('shell:bowerupdate:' + library);
      grunt.task.run('buildbower');
    });
}
