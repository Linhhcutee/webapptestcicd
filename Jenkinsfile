pipeline {
    agent any

    environment {
        DOTNET_CLI_HOME = '/home/jenkins/.dotnet'
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
                    // Kiểm tra nội dung thư mục gốc
                    sh "ls -al /var/lib/jenkins/workspace/webapp/"
                    
                    // Chuyển đến thư mục chứa file .csproj
                    dir('src/QLCuaHangBanSach') { // Điều chỉnh đường dẫn nếu cần
                        // Kiểm tra nội dung thư mục
                        sh "ls -al"

                        // Thêm gói Pomelo.EntityFrameworkCore.MySql
                        sh "dotnet add package Pomelo.EntityFrameworkCore.MySql"

                        // Khôi phục các phụ thuộc
                        sh "dotnet restore"

                        // Xây dựng ứng dụng
                        sh "dotnet build --configuration Release"
                    }
                }
            }
        }

        stage('Test') {
            steps {
                script {
                    dir('src/QLCuaHangBanSach') { // Điều chỉnh đường dẫn nếu cần
                        // Chạy các bài kiểm tra
                        sh "dotnet test --no-restore --configuration Release"
                    }
                }
            }
        }

        stage('Publish') {
            steps {
                script {
                    dir('src/QLCuaHangBanSach') { // Điều chỉnh đường dẫn nếu cần
                        // Xuất bản ứng dụng
                        sh "dotnet publish -c Release --no-restore --output ./publish"
                    }
                }
            }
        }

        stage('Deploy') {
            steps {
                script {
                    dir('src/QLCuaHangBanSach') { // Điều chỉnh đường dẫn nếu cần
                        // Kill process đang chạy (nếu có)
                        sh "fuser -k 5000/tcp || true"

                        // Chạy ứng dụng đã publish với URL lắng nghe trên mọi địa chỉ IP
                        sh "nohup dotnet ./publish/QLCuaHangBanSach.dll --urls=\"http://*:5000;https://*:5001\" > /dev/null 2>&1 &"
                    }
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
