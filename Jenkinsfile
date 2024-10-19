pipeline {
    agent any

    environment {
        DOTNET_CLI_HOME = '/home/jenkins/.dotnet' // Thay đổi đường dẫn này nếu cần
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

        stage('Deploy') {
            steps {
                script {
                    // Kill process đang chạy (nếu có)
                    sh "fuser -k 5000/tcp || true" // Giả sử ứng dụng chạy trên cổng 5000

                    // Chạy ứng dụng đã publish với URL lắng nghe trên mọi địa chỉ IP
                    sh "nohup dotnet ./publish/QLCuaHangBanSach.dll --urls=\"http://*:5000;https://*:5001\" > /dev/null 2>&1 &"
                }
            }
        }
    }

    post {
        success {
            echo 'Build, test, publish, and deploy successful!'
        }
        failure {
            echo 'Build, test, publish, or deploy failed!'
        }
        always {
            echo 'Pipeline finished.'
        }
    }
}
