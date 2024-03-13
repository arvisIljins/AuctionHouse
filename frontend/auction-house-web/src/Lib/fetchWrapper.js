import { handleGetToken } from "@/app/services/authService";

const baseUrl = "http://localhost:6001/";

async function get(url) {
  const request = {
    method: "GET",
    header: await getHeader(),
  };

  const response = await fetch(`${baseUrl}${url}`, request);
  return await handleResponse(response);
}

async function post(url, body) {
  const request = {
    method: "POST",
    header: await getHeader(),
    body: JSON.stringify(body),
  };

  const response = await fetch(`${baseUrl}${url}`, request);
  return await handleResponse(response);
}

async function put(url, body) {
  const request = {
    method: "PUT",
    header: await getHeader(),
    body: JSON.stringify(body),
  };

  const response = await fetch(`${baseUrl}${url}`, request);
  return await handleResponse(response);
}

async function del(url, body) {
  const request = {
    method: "DELETE",
    header: await getHeader(),
  };

  const response = await fetch(`${baseUrl}${url}`, request);
  return await handleResponse(response);
}

async function getHeader() {
  const token = await handleGetToken();
  const headers = {
    "Content-type": "application/json",
  };

  if (token) {
    headers.Authorization = `Bearer ${token}`;
  }
  return headers;
}

async function handleResponse(response) {
  const text = await response.text();
  const data = text && JSON.parse(text);

  if (response.ok) {
    return data || data.success;
  } else {
    const error = {
      message: data.message,
    };
    return error;
  }
}

export const fetchWrapper = {
  get,
  post,
  put,
  del,
};
