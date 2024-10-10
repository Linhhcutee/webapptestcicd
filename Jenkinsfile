pipeline {
    agent any

    environment {
        DOTNET_CLI_HOME = '/home/jenkins/.dotnet' // Thay đổi đường dẫn này
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
                    // Khôi phục các phụ thuộc
                    sh "dotnet restore"

                    // Xây dựng ứng dụng
                    sh "dotnet build --configuration Release"
                }
            }
        }

        stage('Test') {
            steps {
                script {
                    // Chạy các bài kiểm tra
                    sh "dotnet test --no-restore --configuration Release"
                }
            }
        }

        stage('Publish') {
            steps {
                script {
                    // Xuất bản ứng dụng
                    sh "dotnet publish --no-restore --configuration Release --output ./publish"
                }
            }
        }
    }

    post {
        success {
            echo 'Build, test, and publish successful!'
        }
        failure {
            echo 'Build, test, or publish failed!'
        }
        always {
            echo 'Pipeline finished.'
        }
    }
}
