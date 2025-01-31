# URL Shortener API 🏷️

This is a simple URL shortening service built with .NET. The API provides three main endpoints for creating, analyzing, and redirecting shortened URLs.

## 📌 Endpoints

### 1️⃣ Create a Shortened URL (`POST /link/add`)
- Accepts a URL as input.
- Generates a unique shortened URL.
- Stores it in the database with an expiration time.

### 2️⃣ Analyze a Shortened URL (`GET /link/analyze?url={shortened_url}`)
- Retrieves analytics data for a given shortened URL.
- Includes information such as access count and metadata.

### 3️⃣ Redirect to the Original URL (`GET /{shortened_code}`)
- Redirects users from the shortened URL to the original URL.

## 🚀 Features
- ✅ URL validation to ensure valid input.
- ✅ Unique short URL generation.
- ✅ Expiration support for shortened links.
- ✅ Analytics tracking for link usage.

## 🏗️ Installation & Usage

1. **Clone the repository:**
   ```sh
   git clone https://github.com/yigitozbek/url-shorter.git
   cd url-shorter



---

## 3️⃣ **README.md Dosyanı Commit Edip GitHub'a Gönder**
Şimdi terminalde aşağıdaki komutları çalıştırarak **README.md** dosyanı GitHub'a ekleyebilirsin:

```sh
git add README.md
git commit -m "Added README.md with project details"
git push origin main
