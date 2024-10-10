pipeline {
    agent any

    environment {
        DOTNET_CLI_HOME = '/usr/bin/dotnet'
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build') {
            steps {
                script {
                    // Restoring dependencies
                    sh "${DOTNET_CLI_HOME} restore"

                    // Building the application
                    sh "${DOTNET_CLI_HOME} build --configuration Release"
                }
            }
        }

        stage('Test') {
            steps {
                script {
                    // Running tests
                    sh "${DOTNET_CLI_HOME} test --no-restore --configuration Release"
                }
            }
        }

        stage('Publish') {
            steps {
                script {
                    // Publishing the application
                    sh "${DOTNET_CLI_HOME} publish --no-restore --configuration Release --output ./publish"
                }
            }
        }
    }

    post {
        success {
            echo 'Build, test, and publish successful!'
        }
    }
}
