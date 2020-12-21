pipeline {
  agent any
  parameters {
    string(name: 'VERSION', defaultValue: '', description: 'version to deploy on production')
  }
  stages {
    stage('build') {
      steps{
        echo 'build'
      }
    }
    stage('test') {
      steps {
        echo 'test'
      }
    }
  }
}
